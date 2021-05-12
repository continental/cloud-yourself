using CloudYourself.Backend.Gateways.CoreApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Fancy.ResourceLinker;

namespace CloudYourself.Backend.Gateways.CoreApi.AzureSubscriptions
{
    /// <summary>
    /// A controller to proxy requests to azure subscriptions.
    /// </summary>
    /// <seealso cref="CloudYourself.Backend.Gateways.CoreApi.Infrastructure.ProxyController" />
    [ApiController]
    public class AzureSubscriptionsProxyController : ProxyController
    {
        /// <summary>
        /// The connector to the azure subscriptions hub.
        /// </summary>
        private readonly AzureSubscriptionsHubConnector _connector;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureSubscriptionsProxyController"/> class.
        /// </summary>
        /// <param name="connector">The connector.</param>
        /// <param name="appServiceBaseUrls">The application service base urls.</param>
        public AzureSubscriptionsProxyController(AzureSubscriptionsHubConnector connector, IOptions<AppServiceBaseUrlOptions> appServiceBaseUrls)
            : base(appServiceBaseUrls.Value.AzureSubscriptions)
        {
            _connector = connector;
        }

        /// <summary>
        /// Gets a single azure subscription.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <returns>An azure subscription.</returns>
        [HttpGet]
        [Route("api/tenants/{tenantId}/cloud-accounts/{cloudAccountId}/azure-subscriptions/{subscriptionId}")]
        public Task<IActionResult> GetById(int tenantId, int cloudAccountId, int subscriptionId)
        {
            return ProxyGetAsync<AzureSubscriptionVm>();
        }

        /// <summary>
        /// Gets the template for a new subscription.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <returns>The new subscription template.</returns>
        [HttpGet]
        [Route("api/tenants/{tenantId}/cloud-accounts/{cloudAccountId}/azure-subscriptions/template")]
        public async Task<IActionResult> GetNewTemplate(int tenantId, int cloudAccountId)
        {
            NewSubscriptionVm result = await GetAsync<NewSubscriptionVm>();
            result.TenantId = tenantId;
            result.CloudAccountId = cloudAccountId;
            return Hypermedia(result);
        }

        /// <summary>
        /// Creates a new azure subscription.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <returns>The url to the newly created resource.</returns>
        [HttpPost]
        [Route("api/tenants/{tenantId}/cloud-accounts/{cloudAccountId}/azure-subscriptions")]
        public async Task<IActionResult> Create(int tenantId, int cloudAccountId)
        {
            string postResult = await ProxyPostAsync();
            int newEntityId = Convert.ToInt32(postResult);
            string newResourceUrl = Url.LinkTo<AzureSubscriptionsProxyController>(c => c.GetById(tenantId, cloudAccountId, newEntityId));
            return Created(newResourceUrl, new string[] { newResourceUrl });
        }

        /// <summary>
        /// Cancels a subscription by identifier.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        [HttpDelete]
        [Route("api/tenants/{tenantId}/cloud-accounts/{cloudAccountId}/azure-subscriptions/{subscriptionId}")]
        public Task<IActionResult> CancelById(int tenantId, int cloudAccountId, int subscriptionId)
        {
            return ProxyDeleteAsync();
        }
    }
}
