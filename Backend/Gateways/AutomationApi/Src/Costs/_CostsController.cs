using CloudYourself.Backend.Gateways.AutomationApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;
using Fancy.ResourceLinker;

namespace CloudYourself.Backend.Gateways.AutomationApi.Costs
{
    /// <summary>
    /// A controller to proxy requests to azure subscriptions.
    /// </summary>
    /// <seealso cref="CloudYourself.Backend.Gateways.AutomationApi.Infrastructure.ProxyController" />
    [Authorize]
    [ApiController]
    public class CostsController : ProxyController
    {
        /// <summary>
        /// The application service base urls
        /// </summary>
        private readonly IOptions<AppServiceBaseUrlOptions> _appServiceBaseUrls;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureSubscriptionsProxyController"/> class.
        /// </summary>
        /// <param name="connector">The connector.</param>
        /// <param name="appServiceBaseUrls">The application service base urls.</param>
        public CostsController(IOptions<AppServiceBaseUrlOptions> appServiceBaseUrls)
            : base(appServiceBaseUrls.Value.Billing)
        {
            _appServiceBaseUrls = appServiceBaseUrls;
        }

        /// <summary>
        /// Gets the new cost template.
        /// </summary>
        /// <param name="costType">Type of the cost.</param>
        /// <param name="costId">The cost identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/billing/costs/{costType}/{costId}/template")]
        public Task<IActionResult> GetNewTemplate(string costType, string costId, int cloudAccountId, int tenantId)
        {
            return ProxyGetAsync<NewCostDto>();   
        }

        /// <summary>
        /// Creates a new cost asynchronous.
        /// </summary>
        /// <param name="createCostDto">The create cost dto.</param>
        [HttpPost]
        [Route("api/billing/costs")]
        public async Task<IActionResult> CreateNew()
        {
            string postResult = await ProxyPostAsync();
            int newEntityId = Convert.ToInt32(postResult);
            string newResourceUrl = Url.LinkTo<CostsController>(c => c.GetById(newEntityId));
            return Created(newResourceUrl, new string[] { newResourceUrl });
        }

        /// <summary>
        /// Gets the cost by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpGet]
        [Route("api/billing/costs/{id}")]
        public Task<IActionResult> GetById(int id)
        {
            return ProxyGetAsync<CostDto>();
        }

        /// <summary>
        /// Gets a specific cost of a specific type with a specific id of a specific period id asynchronous.
        /// </summary>
        /// <param name="costType">Type of the cost.</param>
        /// <param name="costId">The cost identifier.</param>
        /// <param name="periodId">The period identifier.</param>
        [HttpGet]
        [Route("api/billing/costs/{costType}/{costId}/{periodId}")]
        public Task<IActionResult> GetByPeriod(string costType, string costId, string periodId)
        {
            return ProxyGetAsync<CostDto>();
        }

        /// <summary>
        /// Gets the costs of a specified cost id asynchronous.
        /// </summary>
        /// <param name="costType">Type of the cost.</param>
        /// <param name="costId">The cost identifier.</param>
        /// <param name="periodId">The period identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/billing/costs/{costType}/{costId}")]
        public Task<IActionResult> GetByCostId(string costType, string costId)
        {
            if (HttpContext.Request.Query.ContainsKey("periodId")) return ProxyGetAsync<CostDto>();
            else return ProxyGetCollectionAsync<CostDto>();
        }

        /// <summary>
        /// Updates the cost details asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="costDetails">The cost details.</param>
        [HttpPut]
        [Route("api/billing/costs/{id}")]
        public Task<IActionResult> UpdateCostDetails(int id)
        {
            return ProxyPutAsync();
        }
    }
}
