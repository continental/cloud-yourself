using Azure.Core;
using Azure.Identity;
using CloudYourself.Backend.AppServices.AzureSubscriptions.Aggregates.Tennant;
using CloudYourself.Backend.AppServices.AzureSubscriptions.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.AzureSubscriptions.Services
{
    public class AzureSubscriptionService
    {
        /// <summary>
        /// The scope to request for the token.
        /// </summary>
        private const string SCOPE = "https://management.core.windows.net/.default";

        /// <summary>
        /// The database context.
        /// </summary>
        private readonly AzureSubscriptionsDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureSubscriptionService"/> class.
        /// </summary>
        public AzureSubscriptionService(AzureSubscriptionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Starts the create subscription operation asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier for which to create a subscription.</param>
        /// <param name="subscriptionName">Name of the subscription to create.</param>
        /// <returns>The url to the creation operation.</returns>
        public async Task<string> StartCreateSubscriptionOperationAsync(int tenantId, string subscriptionName)
        {
            Tenant tenant = await _dbContext.Tenants.SingleAsync(t => t.Id == tenantId);

            HttpClient httpClient = await GetAuthenticatedClientAsync(tenantId);

            string url = $"https://management.azure.com/providers/Microsoft.Billing/enrollmentAccounts/{tenant.ManagementTarget.EnrollmentAccountId}/providers/Microsoft.Subscription/createSubscription?api-version=2018-03-01-preview";

            // Set up the subscription requests
            var subscriptionRequest = new {
                displayName = subscriptionName,
                offerType = "MS-AZR-0017P",
                managementGroupId = "/providers/Microsoft.Management/managementGroups/" + tenant.ManagementTarget.ManagementGroupId
            };

            HttpResponseMessage subscriptionCreateResponse = httpClient.PostAsync(url, new ObjectContent(subscriptionRequest)).Result;
            subscriptionCreateResponse.EnsureSuccessStatusCode();

            string creationOperationUrl = subscriptionCreateResponse.Headers.Location.AbsoluteUri;

            return creationOperationUrl;
        }

        /// <summary>
        /// Gets the subscription link asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="creationOperationUrl">The creation operation URL.</param>
        /// <returns>The subscription link.</returns>
        public async Task<string> GetSubscriptionLinkAsync(int tenantId, string creationOperationUrl)
        {
            HttpClient httpClient = await GetAuthenticatedClientAsync(tenantId);

            HttpResponseMessage creationResponse = await httpClient.GetAsync(creationOperationUrl);

            if (creationResponse.StatusCode == HttpStatusCode.OK)
            {
                // Subscription was created successfully
                string jsonResponse = await creationResponse.Content.ReadAsStringAsync();
                JsonDocument response = JsonDocument.Parse(jsonResponse);
                string subscriptionLink = response.RootElement.GetProperty("subscriptionLink").GetString();
                return subscriptionLink;
            }
            else
            {
                // Subscription creation is still pending
                return null;
            }
        }

        /// <summary>
        /// Cancels an existing subscription asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="subscriptionLink">The subscription link.</param>
        public async Task CancelSubscriptionAsync(int tenantId, string subscriptionLink)
        {
            HttpClient httpClient = await GetAuthenticatedClientAsync(tenantId);

            string cancelUrl = "https://management.azure.com" + subscriptionLink + "/providers/Microsoft.Subscription/cancel?api-version=2019-03-01-preview";

            HttpResponseMessage cancelResponse = httpClient.PostAsync(cancelUrl, new StringContent(string.Empty)).Result;

            cancelResponse.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Gets an authenticated http client to a specified tenant asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns>An http client with authentication information.</returns>
        private async Task<HttpClient> GetAuthenticatedClientAsync(int tenantId)
        {
            Tenant tenant = await _dbContext.Tenants.SingleAsync(t => t.Id == tenantId);

            ClientSecretCredential credential = new ClientSecretCredential(tenant.AppRegistration.AzureDirectoryTenantId,
                                                                           tenant.AppRegistration.AzureAppRegistrationId,
                                                                           tenant.AppRegistration.AzureAppSecret);

            TokenRequestContext tokenRequestContext = new TokenRequestContext(new[] { SCOPE });
            AccessToken accessToken = await credential.GetTokenAsync(tokenRequestContext);

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Token);

            return httpClient;
        }

        /// <summary>
        /// A class to easily convert an object to a json content. 
        /// </summary>
        class ObjectContent : StringContent
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ObjectContent"/> class.
            /// </summary>
            /// <param name="obj">The object.</param>
            public ObjectContent(object obj) :
                base(JsonSerializer.Serialize(obj, obj.GetType()), Encoding.UTF8, "application/json")
            { }
        }
    }
}
