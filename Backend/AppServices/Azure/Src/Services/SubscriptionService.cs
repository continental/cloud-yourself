using CloudYourself.Backend.AppServices.Azure.Aggregates.Tennant;
using CloudYourself.Backend.AppServices.Azure.Dtos;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.Azure.Services
{
    public class SubscriptionService : AzureServiceBase
    {
        /// <summary>
        /// Gets all unmanaged subscriptions of a specific tenant asynchronous.
        /// </summary>
        /// <returns>A list of unmanaged subscriptions.</returns>
        public async Task<List<UnmanagedSubscriptionDto>> GetAllUnmanagedByTenantIdAsync(TenantSettings tenantSettings)
        {
            HttpClient httpClient = await GetAuthenticatedClientAsync(tenantSettings);

            string url = "https://management.azure.com/subscriptions?api-version=2020-01-01";

            HttpResponseMessage getSubscriptionsResponse = await httpClient.GetAsync(url);
            getSubscriptionsResponse.EnsureSuccessStatusCode();

            string jsonResponse = await getSubscriptionsResponse.Content.ReadAsStringAsync();
            JsonDocument response = JsonDocument.Parse(jsonResponse);

            List<UnmanagedSubscriptionDto> result = new List<UnmanagedSubscriptionDto>();

            // Loop through each subscription and create dto
            foreach(JsonElement subscription in response.RootElement.GetProperty("value").EnumerateArray())
            {
                // Process only enabled subscriptions
                if (subscription.GetProperty("state").GetString() == "Enabled")
                {
                    result.Add(new UnmanagedSubscriptionDto
                    {
                        Name = subscription.GetProperty("displayName").GetString(),
                        SubscriptionId = subscription.GetProperty("subscriptionId").GetString(),
                        SubscriptionLink = subscription.GetProperty("id").GetString(),
                        TenantId = tenantSettings.Id
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// Starts the create subscription operation asynchronous.
        /// </summary>
        /// <param name="tenantSettings">The tenant settings.</param>
        /// <param name="subscriptionName">Name of the subscription to create.</param>
        /// <returns>
        /// The url to the creation operation.
        /// </returns>
        public async Task<string> StartCreateSubscriptionOperationAsync(TenantSettings tenantSettings, string subscriptionName)
        {
            HttpClient httpClient = await GetAuthenticatedClientAsync(tenantSettings);

            string url = $"https://management.azure.com/providers/Microsoft.Billing/enrollmentAccounts/{tenantSettings.ManagementTarget.EnrollmentAccountId}/providers/Microsoft.Subscription/createSubscription?api-version=2018-03-01-preview";

            // Set up the subscription requests
            var subscriptionRequest = new {
                displayName = subscriptionName,
                offerType = "MS-AZR-0017P",
                managementGroupId = "/providers/Microsoft.Management/managementGroups/" + tenantSettings.ManagementTarget.ManagementGroupId
            };

            HttpResponseMessage subscriptionCreateResponse = httpClient.PostAsync(url, new ObjectContent(subscriptionRequest)).Result;
            subscriptionCreateResponse.EnsureSuccessStatusCode();

            string creationOperationUrl = subscriptionCreateResponse.Headers.Location.AbsoluteUri;

            return creationOperationUrl;
        }

        /// <summary>
        /// Gets the subscription link asynchronous.
        /// </summary>
        /// <param name="tenantSettings">The tenant settings.</param>
        /// <param name="creationOperationUrl">The creation operation URL.</param>
        /// <returns>
        /// The subscription link.
        /// </returns>
        public async Task<string> GetSubscriptionLinkAsync(TenantSettings tenantSettings, string creationOperationUrl)
        {
            HttpClient httpClient = await GetAuthenticatedClientAsync(tenantSettings);

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
        /// <param name="tenantSettings">The tenant settings.</param>
        /// <param name="subscriptionLink">The subscription link.</param>
        public async Task CancelSubscriptionAsync(TenantSettings tenantSettings, string subscriptionLink)
        {
            HttpClient httpClient = await GetAuthenticatedClientAsync(tenantSettings);

            string cancelUrl = "https://management.azure.com" + subscriptionLink + "/providers/Microsoft.Subscription/cancel?api-version=2019-03-01-preview";

            HttpResponseMessage cancelResponse = httpClient.PostAsync(cancelUrl, new StringContent(string.Empty)).Result;

            cancelResponse.EnsureSuccessStatusCode();
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
