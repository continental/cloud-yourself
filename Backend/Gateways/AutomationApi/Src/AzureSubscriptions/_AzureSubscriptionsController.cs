using CloudYourself.Backend.Gateways.AutomationApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CloudYourself.Backend.Gateways.AutomationApi.AzureSubscriptions
{
    /// <summary>
    /// A controller to proxy requests to azure subscriptions.
    /// </summary>
    /// <seealso cref="CloudYourself.Backend.Gateways.AutomationApi.Infrastructure.ProxyController" />
    [Authorize]
    [ApiController]
    public class AzureSubscriptionsController : ProxyController
    {
        /// <summary>
        /// The application service base urls
        /// </summary>
        private readonly IOptions<AppServiceBaseUrlOptions> _appServiceBaseUrls;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureSubscriptionsController"/> class.
        /// </summary>
        /// <param name="connector">The connector.</param>
        /// <param name="appServiceBaseUrls">The application service base urls.</param>
        public AzureSubscriptionsController(IOptions<AppServiceBaseUrlOptions> appServiceBaseUrls)
            : base(appServiceBaseUrls.Value.Azure)
        {
            _appServiceBaseUrls = appServiceBaseUrls;
        }

        /// <summary>
        /// Gets a subscription by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpGet]
        [Route("api/azure/subscriptions/{id}")]
        public Task<IActionResult> GetById(int id)
        {
            return ProxyGetAsync<AzureSubscriptionDto>();
        }

        /// <summary>
        /// Gets subscirptions filtered.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <returns>ü
        /// A list of subscriptions.
        /// </returns>
        [HttpGet]
        [Route("api/azure/subscriptions")]
        public Task<IActionResult> GetFiltered(int? tenantId = null, int? cloudAccountId = null)
        {
            return ProxyGetCollectionAsync<AzureSubscriptionDto>();
        }

        /// <summary>
        /// Updates the compliance data of a subscription.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="complianceState">Data of the compliance.</param>
        [HttpPut]
        [Route("api/azure/subscriptions/{id}/compliance")]
        public Task<IActionResult> UpdateCompliance(int id)
        {
            return ProxyPutAsync();
        }

        /// <summary>
        /// Cancels a subscription by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/azure/subscriptions/{id}")]
        public Task<IActionResult> CancelById(int id)
        {
            return ProxyDeleteAsync();
        }
    }
}
