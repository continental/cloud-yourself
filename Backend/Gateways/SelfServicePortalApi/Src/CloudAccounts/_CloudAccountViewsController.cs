using CloudYourself.Backend.Gateways.SelfServicePortalApi.Infrastructure;
using Fancy.ResourceLinker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Fancy.ResourceLinker;
using CloudYourself.Backend.Gateways.SelfServicePortalApi.AzureSubscriptions;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.CloudAccounts
{
    /// <summary>
    /// A controller to proxy requests to the cloud accounts.
    /// </summary>
    /// <seealso cref="ProxyController" />
    [Authorize]
    [ApiController]
    public class CloudAccountViewsController : ProxyController
    {
        /// <summary>
        /// The application service base urls.
        /// </summary>
        private readonly AppServiceBaseUrlOptions _appServiceBaseUrls;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudAccountViewsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public CloudAccountViewsController(IOptions<AppServiceBaseUrlOptions> appServiceBaseUrls)
            : base(appServiceBaseUrls.Value.MasterData)
        {
            _appServiceBaseUrls = appServiceBaseUrls.Value;
        }

        /// <summary>
        /// Gets the list cloud accounts view mdoel
        /// </summary>
        /// <returns>The list cloud accounts view model.</returns>
        [HttpGet]
        [Route("/api/views/cloud-accounts/list")]
        public async Task<IActionResult> GetListViewModel()
        {
            ListCloudAccountsVm result = new ListCloudAccountsVm();
            result.CloudAccounts = await GetCollectionAsync<DynamicResource>(null, "/api/master-data/cloud-accounts");
            foreach(dynamic ca in result.CloudAccounts)
            {
                ca.Tenant = await GetAsync<DynamicResource>(null, $"/api/master-data/tenants/{ca.TenantId}");
                ca.AzureSubscriptionsCount = (await GetCollectionAsync<DisplayAzureSubscriptionVm>(_appServiceBaseUrls.Azure, 
                                                                                               $"/api/azure/subscriptions?cloudAccountId={ca.Id}")).Count;
            }
            return Hypermedia(result);
        }

        /// <summary>
        /// Gets the display cloud accounts view mdoel
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The display cloud accounts view model.
        /// </returns>
        [HttpGet]
        [Route("/api/views/cloud-accounts/{id}/display/")]
        public async Task<IActionResult> GetDisplayViewModel(int id)
        {
            DisplayCloudAccountVm result = await GetAsync<DisplayCloudAccountVm>(null, $"/api/master-data/cloud-accounts/{id}");
            result["Tenant"] = await GetAsync<DynamicResource>(null, $"/api/master-data/tenants/{result.TenantId}");
            result["AzureSubscriptions"] = await GetCollectionAsync<DynamicResource>(_appServiceBaseUrls.Azure, $"/api/azure/subscriptions?cloudAccountId={id}");
            result["AwsAccounts"] = await GetCollectionAsync<DynamicResource>(_appServiceBaseUrls.Aws, $"/api/aws/accounts?cloudAccountId={id}");
            List<DynamicResource> allocationKeys = await GetCollectionAsync<DynamicResource>(_appServiceBaseUrls.Billing, $"/api/billing/allocation-keys?cloudAccountId={id}");
            foreach(dynamic allocationKey in allocationKeys)
            {
                allocationKey["PayerAccount"] = await GetAsync<DynamicResource>(_appServiceBaseUrls.Billing,
                                                                                $"/api/billing/payer-accounts/{allocationKey.PayerAccountId}");
            }

            result["AllocationKeys"] = allocationKeys;

            return Hypermedia(result);
        }

        /// <summary>
        /// Gets the edit cloud accounts view mdoel
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The edit cloud accounts view model.
        /// </returns>
        [HttpGet]
        [Route("/api/views/cloud-accounts/{id}/edit/")]
        public async Task<IActionResult> GetEditViewModel(int id)
        {
            EditCloudAccountVm result = await GetAsync<EditCloudAccountVm>(null, $"/api/master-data/cloud-accounts/{id}");
            result["Tenant"] = await GetAsync<DynamicResource>(null, $"/api/master-data/tenants/{result.TenantId}");
            return Hypermedia(result);
        }

        /// <summary>
        /// Gets the request cloud accounts view mdoel
        /// </summary>
        /// <returns>The request cloud accounts view model.</returns>
        [HttpGet]
        [Route("api/views/cloud-accounts/request")]
        public async Task<IActionResult> GetRequestViewModel()
        {
            RequestCloudAccountVm result = new RequestCloudAccountVm();
            result.Tenants = await GetCollectionAsync<DynamicResource>(null, "/api/master-data/tenants");
            result.Template = await GetAsync<DynamicResource>(null, "/api/master-data/cloud-accounts/request");
            return Hypermedia(result);
        }

        /// <summary>
        /// Creates a new cloud account.
        /// </summary>
        /// <returns>The url to the new cloud account.</returns>
        [HttpPost]
        [Route("api/master-data/cloud-accounts")]
        public async Task<IActionResult> CreateRequest()
        {
            string postResult = await ProxyPostAsync();
            int newEntityId = Convert.ToInt32(postResult);
            string newResourceUrl = Url.LinkTo<CloudAccountViewsController>(c => c.GetDisplayViewModel(newEntityId));
            return Created(newResourceUrl, new string[] { newResourceUrl });
        }

        /// <summary>
        /// Sets the state of a cloud account.
        /// </summary>
        /// <param name="newState">The new state.</param>
        [HttpPost]
        [Route("/api/master-data/cloud-accounts/state")]
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
        [Route("/api/master-data/cloud-accounts/{id}/base-data")]
        public Task<IActionResult> UpdateBaseData(int id)
        {
            return ProxyPutAsync();
        }
    }
}
