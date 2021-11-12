using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Fancy.ResourceLinker;
using System;
using Fancy.ResourceLinker.Models;
using CloudYourself.Backend.Gateways.SelfServicePortalApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.PayerAccounts
{
    /// <summary>
    /// A controller to proxy requests to the payer accounts.
    /// </summary>
    [Authorize]
    [ApiController]
    public class PayerAccountViewsController : ProxyController
    {
        /// <summary>
        /// The application service base urls.
        /// </summary>
        private readonly AppServiceBaseUrlOptions _appServiceBaseUrls;

        /// <summary>
        /// Initializes a new instance of the <see cref="PayerAccountViewsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public PayerAccountViewsController(IOptions<AppServiceBaseUrlOptions> appServiceBaseUrls)
            : base(appServiceBaseUrls.Value.Billing)
        {
            _appServiceBaseUrls = appServiceBaseUrls.Value;
        }

        /// <summary>
        /// Gets the list payer accounts view model
        /// </summary>
        /// <param name="tenantId">The tenant identifier to show the payer accounts for.</param>
        /// <returns>
        /// The list payer accounts view model.
        /// </returns>
        [HttpGet]
        [Route("/api/views/payer-accounts/list")]
        public async Task<IActionResult> GetListViewModel(int tenantId)
        {
            ListPayerAccountsVm result = new ListPayerAccountsVm { TenantId = tenantId };
            result.PayerAccounts = await GetCollectionAsync<DynamicResource>(null, $"/api/billing/payer-accounts?tenantId={tenantId}");
            return Hypermedia(result);
        }

        /// <summary>
        /// Gets the display payer account view mdoel
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The display payer account view model.
        /// </returns>
        [HttpGet]
        [Route("/api/views/payer-accounts/display/{id}")]
        public async Task<IActionResult> GetDisplayViewModel(int id)
        {
            DisplayPayerAccountVm result  = await GetAsync<DisplayPayerAccountVm>(null, $"/api/billing/payer-accounts/{id}");
            return Hypermedia(result);
        }

        /// <summary>
        /// Gets the edit payer account view mdoel
        /// </summary>
        /// <returns>The edit payer account view model.</returns>
        [HttpGet]
        [Route("/api/billing/payer-accounts/edit/{id}")]
        public async Task<IActionResult> GetEditViewModel(int id)
        {
            EditPayerAccountVm result = await GetAsync<EditPayerAccountVm>(null, $"/api/billing/payer-accounts/{id}");
            return Hypermedia(result);
        }

        /// <summary>
        /// Gets a template for a new payer account
        /// </summary>
        /// <returns>A new payer account template.</returns>
        [HttpGet]
        [Route("api/views/payer-accounts/create")]
        public async Task<IActionResult> GetCreateViewModel(int tenantId)
        {
            CreatePayerAccountVm result = await GetAsync<CreatePayerAccountVm>(null, $"/api/billing/payer-accounts/template?tenantId={tenantId}");
            result.TenantId = tenantId;

            return Hypermedia(result);
        }

        /// <summary>
        /// Creates a new payer account with the specified data.
        /// </summary>
        [HttpPost]
        [Route("api/billing/payer-accounts")]
        public async Task<IActionResult> Create()
        {
            string postResult = await ProxyPostAsync();
            int newEntityId = Convert.ToInt32(postResult);
            string newResourceUrl = Url.LinkTo<PayerAccountViewsController>(c => c.GetDisplayViewModel(newEntityId));
            return Created(newResourceUrl, new string[] { newResourceUrl });
        }

        /// <summary>
        /// Deletes a payer account resource by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete]
        [Route("api/billing/payer-accounts/{id}")]
        public Task<IActionResult> DeleteById(int id)
        {
            return ProxyDeleteAsync();
        }

        /// <summary>
        /// Updates the base data of a payer account.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="baseData">The base data.</param>
        [HttpPut]
        [Route("api/billing/payer-accounts/{id}/base-data")]
        public Task<IActionResult> UpdateBaseData(int id)
        {
            return ProxyPutAsync();
        }
    }
}
