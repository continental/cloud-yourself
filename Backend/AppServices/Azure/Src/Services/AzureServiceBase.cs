using Azure.Core;
using Azure.Identity;
using CloudYourself.Backend.AppServices.Azure.Aggregates.Tennant;
using CloudYourself.Backend.AppServices.Azure.Infrastructure;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.Azure.Services
{
    /// <summary>
    /// Base class for azure services.
    /// </summary>
    public class AzureServiceBase
    {
        /// <summary>
        /// The scope to request for the token.
        /// </summary>
        private const string SCOPE = "https://management.core.windows.net/.default";

        /// <summary>
        /// Gets an authenticated http client to a specified tenant asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns>An http client with authentication information.</returns>
        protected async Task<HttpClient> GetAuthenticatedClientAsync(TenantSettings tenantSettings)
        {
            ClientSecretCredential credential = new ClientSecretCredential(tenantSettings.AppRegistration.AzureDirectoryTenantId,
                                                                           tenantSettings.AppRegistration.AzureAppRegistrationId,
                                                                           tenantSettings.AppRegistration.AzureAppSecret);

            TokenRequestContext tokenRequestContext = new TokenRequestContext(new[] { SCOPE });
            AccessToken accessToken = await credential.GetTokenAsync(tokenRequestContext);

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Token);

            return httpClient;
        }

        /// <summary>
        /// Throws the error object exception.
        /// </summary>
        /// <param name="errorJson">The error json.</param>
        protected void ThrowErrorObjectException(string errorJson)
        {
            JsonElement errorObject = JsonDocument.Parse(errorJson).RootElement.GetProperty("error");
            string errorCode = errorObject.GetProperty("code").GetString();
            string errorMessage = errorObject.GetProperty("message").GetString();
            throw new ErrorObjectException(errorCode, errorMessage);
        }
    }
}
