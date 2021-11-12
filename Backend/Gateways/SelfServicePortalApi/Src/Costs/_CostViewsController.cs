using CloudYourself.Backend.Gateways.SelfServicePortalApi.Infrastructure;
using Fancy.ResourceLinker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.Costs
{
    [Authorize]
    [ApiController]
    public class CostViewsController : ProxyController
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<CostViewsController> _logger;

        /// <summary>
        /// The application service base urls.
        /// </summary>
        private readonly AppServiceBaseUrlOptions _appServiceBaseUrls;

        /// <summary>
        /// Initializes a new instance of the <see cref="CostViewsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public CostViewsController(ILogger<CostViewsController> logger, IOptions<AppServiceBaseUrlOptions> appServiceBaseUrls)
            : base(appServiceBaseUrls.Value.Billing)
        {
            _logger = logger;
            _appServiceBaseUrls = appServiceBaseUrls.Value;
        }

        /// <summary>
        /// Gets the cost matrix view model.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns>
        /// The cost matrix view model.
        /// </returns>
        [HttpGet]
        [Route("/api/views/costs/cost-matrix")]
        public async Task<IActionResult> GetCostMatrixViewModel(int tenantId)
        {
            CostMatrixVm result = new CostMatrixVm();
            result.TenantId = tenantId;
            result.PayerAccountCosts = await GetCollectionAsync<DynamicResource>(null, $"/api/billing/payer-accounts/costs?tenantId={tenantId}");
            return Hypermedia(result);
        }

        [HttpGet]
        [Route("api/billing/payer-accounts/{payerAccountId}/cloud-accounts/costs")]
        public async Task<IActionResult> GetCloudAccountCosts(int payerAccountId, string currency)
        {
            List<CloudAccountCostDto> result = await GetCollectionAsync<CloudAccountCostDto>();

            foreach(CloudAccountCostDto cloudAccountCost in result)
            {
                dynamic cloudAccountMasterData = await GetAsync<DynamicResource>(_appServiceBaseUrls.MasterData, $"/api/master-data/cloud-accounts/{cloudAccountCost.CloudAccountId}");
                cloudAccountCost["CloudAccountName"] = cloudAccountMasterData.BaseData.Name;
            }

            return Hypermedia(result);
        }

        [HttpGet]
        [Route("api/billing/payer-accounts/{payerAccountId}/cloud-accounts/{cloudAccountId}/costs")]
        public Task<IActionResult> GetCosts(int payerAccountId, int cloudAccountId, string currency)
        {
            return ProxyGetCollectionAsync<DynamicResource>();
        }
    }
}
