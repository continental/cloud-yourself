using CloudYourself.Backend.Gateways.CoreApi.Infrastructure;
using CloudYourself.Backend.Gateways.CoreApi.Tenants;
using Fancy.ResourceLinker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Fancy.ResourceLinker;
using CloudYourself.Backend.Gateways.CoreApi.AzureSubscriptions;

namespace CloudYourself.Backend.Gateways.CoreApi.CloudAccounts
{
    /// <summary>
    /// A controller to proxy requests to the cloud accounts.
    /// </summary>
    /// <seealso cref="CloudYourself.Backend.Gateways.CoreApi.Infrastructure.ProxyController" />
    [ApiController]
    public class CloudAccountsProxyController : ProxyController
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<CloudAccountsProxyController> _logger;

        /// <summary>
        /// The application service base urls.
        /// </summary>
        private readonly IOptions<AppServiceBaseUrlOptions> _appServiceBaseUrls;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudAccountsProxyController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public CloudAccountsProxyController(ILogger<CloudAccountsProxyController> logger, IOptions<AppServiceBaseUrlOptions> appServiceBaseUrls)
            : base(appServiceBaseUrls.Value.CloudAccounts)
        {
            _logger = logger;
            _appServiceBaseUrls = appServiceBaseUrls;
        }

        /// <summary>
        /// Gets all cloud accounts.
        /// </summary>
        /// <returns>The cloud accounts.</returns>
        [HttpGet]
        [Route("/api/cloud-accounts")]
        public async Task<IActionResult> GetAll()
        {
            CloudAccountsVm result = new CloudAccountsVm();
            result.CloudAccounts = await GetCollectionAsync<CloudAccountVm>();
            foreach(CloudAccountVm ca in result.CloudAccounts)
            {
                ca["Tenant"] = await GetAsync<TenantVm>(null, $"/api/tenants/{ca.TenantId}");
                ca["AzureSubscriptionsCount"] = (await GetCollectionAsync<AzureSubscriptionVm>(_appServiceBaseUrls.Value.AzureSubscriptions, $"/api/tenants/{ca.TenantId}/cloud-accounts/{ca.Id}/azure-subscriptions")).Count;
            }
            return Hypermedia(result);
        }

        /// <summary>
        /// Gets a specific cloud account identified by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A cloud account.</returns>
        [HttpGet]
        [Route("/api/cloud-accounts/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CloudAccountVm result = await GetAsync<CloudAccountVm>();
            result["Tenant"] = await GetAsync<TenantVm>(null, $"/api/tenants/{result.TenantId}");
            result["AzureSubscriptions"] = await GetCollectionAsync<AzureSubscriptionVm>(_appServiceBaseUrls.Value.AzureSubscriptions, $"/api/tenants/{result.TenantId}/cloud-accounts/{id}/azure-subscriptions");
            return Hypermedia(result);
        }

        /// <summary>
        /// Gets the cloud account request template.
        /// </summary>
        /// <returns>A template for a cloud account request.</returns>
        [HttpGet]
        [Route("api/cloud-accounts/request")]
        public async Task<IActionResult> GetRequest()
        {
            CloudAccountRequestVm result = new CloudAccountRequestVm();
            result.Tenants = await GetCollectionAsync<TenantVm>(null, "/api/tenants");
            result.Template = await GetAsync<DynamicResource>();
            return Hypermedia(result);
        }

        /// <summary>
        /// Creates a new cloud account.
        /// </summary>
        /// <returns>The url to the new cloud account.</returns>
        [HttpPost]
        [Route("api/cloud-accounts")]
        public async Task<IActionResult> Create()
        {
            string postResult = await ProxyPostAsync();
            int newEntityId = Convert.ToInt32(postResult);
            string newResourceUrl = Url.LinkTo<CloudAccountsProxyController>(c => c.GetById(newEntityId));
            return Created(newResourceUrl, new string[] { newResourceUrl });
        }

        /// <summary>
        /// Sets the state of a cloud account.
        /// </summary>
        /// <param name="newState">The new state.</param>
        [HttpPost]
        [Route("/api/cloud-accounts/state")]
        public async Task<IActionResult> SetState(string newState)
        {
            await ProxyPostAsync();
            return NoContent();
        }

        /// <summary>
        /// Updates the base data of a cloud account.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpPut]
        [Route("/api/cloud-accounts/{id}/base-data")]
        public Task<IActionResult> UpdateBaseData(int id)
        {
            return ProxyPutAsync();
        }
    }
}
