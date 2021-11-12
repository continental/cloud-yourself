using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Fancy.ResourceLinker;
using System;
using Fancy.ResourceLinker.Models;
using CloudYourself.Backend.Gateways.SelfServicePortalApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.AllocationKeys
{
    /// <summary>
    /// A controller to proxy requests to the allocation keys.
    /// </summary>
    [Authorize]
    [ApiController]
    public class AllocationKeyViewsController : ProxyController
    {
        /// <summary>
        /// The application service base urls.
        /// </summary>
        private readonly AppServiceBaseUrlOptions _appServiceBaseUrls;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllocationKeyViewsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public AllocationKeyViewsController(IOptions<AppServiceBaseUrlOptions> appServiceBaseUrls)
            : base(appServiceBaseUrls.Value.Billing)
        {
            _appServiceBaseUrls = appServiceBaseUrls.Value;
        }

        /// <summary>
        /// Gets the list allocation keys view model
        /// </summary>
        /// <param name="cloudAccountId">The cloud account identifier to create the allocation key for.</param>
        /// <returns>
        /// The list allocation Keys view model.
        /// </returns>
        [HttpGet]
        [Route("/api/views/allocation-keys/list")]
        public async Task<IActionResult> GetListViewModel(int cloudAccountId)
        {
            ListAllocationKeysVm result = new ListAllocationKeysVm { CloudAccountId = cloudAccountId };
            result.AllocationKeys = await GetCollectionAsync<DynamicResource>(null, $"/api/billing/allocation-keys?cloudAccountId={cloudAccountId}");
            return Hypermedia(result);
        }

        /// <summary>
        /// Gets the display allocation key view mdoel
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The display allocation key view model.
        /// </returns>
        [HttpGet]
        [Route("/api/views/allocation-keys/display/{id}")]
        public async Task<IActionResult> GetDisplayViewModel(int id)
        {
            DisplayAllocationKeyVm result  = await GetAsync<DisplayAllocationKeyVm>(null, $"/api/billing/allocation-keys/{id}");
            result["CloudAccount"] = await GetAsync<DynamicResource>(_appServiceBaseUrls.MasterData, $"/api/master-data/cloud-accounts/{result.CloudAccountId}");
            result["PayerAccount"] = await GetAsync<DynamicResource>(null, $"/api/billing/payer-accounts/{result.PayerAccountId}");
            return Hypermedia(result);
        }

        /// <summary>
        /// Gets the edit allocation key view mdoel
        /// </summary>
        /// <returns>The edit allocation key view model.</returns>
        [HttpGet]
        [Route("/api/billing/allocation-keys/edit/{id}")]
        public async Task<IActionResult> GetEditViewModel(int id)
        {
            EditAllocationKeyVm result = await GetAsync<EditAllocationKeyVm>(null, $"/api/billing/allocation-keys/{id}");
            return Hypermedia(result);
        }

        /// <summary>
        /// Gets a template for a new allocation key
        /// </summary>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <returns>
        /// A new allocation key template.
        /// </returns>
        [HttpGet]
        [Route("api/views/allocation-keys/create")]
        public async Task<IActionResult> GetCreateViewModel(int cloudAccountId, int tenantId)
        {
            CreateAllocationKeyVm result = await GetAsync<CreateAllocationKeyVm>(null, $"/api/billing/allocation-keys/template?cloudAccountId={cloudAccountId}");
            result["availablePayerAccounts"] = await GetCollectionAsync<DynamicResource>(null, $"/api/billing/payer-accounts?tenantId={tenantId}");
            result.TenantId = tenantId;

            return Hypermedia(result);
        }

        /// <summary>
        /// Creates a new allocation key with the specified data.
        /// </summary>
        [HttpPost]
        [Route("api/billing/allocation-keys")]
        public async Task<IActionResult> Create()
        {
            string postResult = await ProxyPostAsync();
            int newEntityId = Convert.ToInt32(postResult);
            string newResourceUrl = Url.LinkTo<AllocationKeyViewsController>(c => c.GetDisplayViewModel(newEntityId));
            return Created(newResourceUrl, new string[] { newResourceUrl });
        }

        /// <summary>
        /// Deletes an allocation key resource by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete]
        [Route("api/billing/allocation-keys/{id}")]
        public Task<IActionResult> DeleteById(int id)
        {
            return ProxyDeleteAsync();
        }

        /// <summary>
        /// Updates the base data of an allocation key.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="baseData">The base data.</param>
        [HttpPut]
        [Route("api/billing/allocation-keys/{id}/base-data")]
        public Task<IActionResult> UpdateBaseData(int id)
        {
            return ProxyPutAsync();
        }
    }
}
