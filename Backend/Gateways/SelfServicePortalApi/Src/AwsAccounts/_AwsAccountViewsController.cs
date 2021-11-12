using CloudYourself.Backend.Gateways.SelfServicePortalApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Fancy.ResourceLinker;
using Fancy.ResourceLinker.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.AwsAccounts
{
    /// <summary>
    /// A controller to proxy requests to azure subscriptions.
    /// </summary>
    /// <seealso cref="CloudYourself.Backend.Gateways.SelfServicePortalApi.Infrastructure.ProxyController" />
    [Authorize]
    [ApiController]
    public class AwsAccountViewsController : ProxyController
    {
        /// <summary>
        /// The application service base urls
        /// </summary>
        private readonly AppServiceBaseUrlOptions _appServiceBaseUrls;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureSubscriptionViewsController" /> class.
        /// </summary>
        /// <param name="appServiceBaseUrls">The application service base urls.</param>
        public AwsAccountViewsController(IOptions<AppServiceBaseUrlOptions> appServiceBaseUrls)
            : base(appServiceBaseUrls.Value.Aws)
        {
            _appServiceBaseUrls = appServiceBaseUrls.Value;
        }

        /// <summary>
        /// Gets a single aws account.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <param name="accountId">The account identifier.</param>
        /// <returns>An aws account.</returns>
        [HttpGet]
        [Route("api/views/aws-accounts/{id}/display")]
        public async Task<IActionResult> GetDisplayViewModel(int id)
        {
            DisplayAwsAccountVm result = await GetAsync<DisplayAwsAccountVm>(null, $"/api/aws/accounts/{id}");
            List<DynamicResource> cost = await GetAsync<List<DynamicResource>>(_appServiceBaseUrls.Billing, $"/api/billing/costs/AwsAccount/{result["AwsAccountId"]}");

            DynamicResource currentCost = null;
            DynamicResource previousCost = null;

            if (cost.Count >= 1)
            {
                dynamic lastCost = cost[0];
                DateTime lastCostEndDate = DateTime.Parse(lastCost.CostDetails.PeriodEnd);

                currentCost = lastCostEndDate.Date >= DateTime.Now.Date ? lastCost : null;
                previousCost = currentCost != null && cost.Count >= 2 ? cost[1] : cost[0];
            }

            result["CurrentCost"] = currentCost;
            result["PreviousCost"] = previousCost;
            return Hypermedia(result);
        }

        /// <summary>
        /// Gets the template for a new account.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <returns>The new account template.</returns>
        [HttpGet]
        [Route("api/views/aws-accounts/add")]
        public async Task<IActionResult> GetAddViewModel(int tenantId, int cloudAccountId)
        {
            dynamic result = new AddAccountVm { TenantId = tenantId, CloudAccountId = cloudAccountId };
            //result.NewTemplate = await GetAsync<DynamicResource>(null, $"/api/aws/accounts/template?tenantId={tenantId}&cloudAccountId={cloudAccountId}");
            result.UnmanagedAccounts = await GetCollectionAsync<DynamicResource>(null, $"/api/aws/accounts/unmanaged?tenantId={tenantId}");

            // Set tenant and cloud account to unmanaged accounts for potentially adding them
            foreach (dynamic account in result.UnmanagedAccounts)
            {
                account.TenantId = tenantId;
                account.CloudAccountId = cloudAccountId;
            }

            return Hypermedia(result);
        }

        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <returns>
        /// The link to the new account.
        /// </returns>
        [HttpPost]
        [Route("api/aws/accounts")]
        public async Task<IActionResult> Create()
        {
            string postResult = await ProxyPostAsync();
            int newEntityId = Convert.ToInt32(postResult);
            string newResourceUrl = Url.LinkTo<AwsAccountViewsController>(c => c.GetDisplayViewModel(newEntityId));
            return Created(newResourceUrl, new string[] { newResourceUrl });
        }
    }
}
