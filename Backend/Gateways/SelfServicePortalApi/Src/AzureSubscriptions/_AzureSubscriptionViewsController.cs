using CloudYourself.Backend.Gateways.SelfServicePortalApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Fancy.ResourceLinker;
using Fancy.ResourceLinker.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.AzureSubscriptions
{
    /// <summary>
    /// A controller to proxy requests to azure subscriptions.
    /// </summary>
    /// <seealso cref="CloudYourself.Backend.Gateways.SelfServicePortalApi.Infrastructure.ProxyController" />
    [Authorize]
    [ApiController]
    public class AzureSubscriptionViewsController : ProxyController
    {
        /// <summary>
        /// The connector to the azure subscriptions hub.
        /// </summary>
        private readonly AzureSubscriptionsHubConnector _connector;

        /// <summary>
        /// The application service base urls
        /// </summary>
        private readonly AppServiceBaseUrlOptions _appServiceBaseUrls;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureSubscriptionViewsController"/> class.
        /// </summary>
        /// <param name="connector">The connector.</param>
        /// <param name="appServiceBaseUrls">The application service base urls.</param>
        public AzureSubscriptionViewsController(AzureSubscriptionsHubConnector connector, IOptions<AppServiceBaseUrlOptions> appServiceBaseUrls)
            : base(appServiceBaseUrls.Value.Azure)
        {
            _connector = connector;
            _appServiceBaseUrls = appServiceBaseUrls.Value;
        }

        /// <summary>
        /// Gets a single azure subscription.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <returns>An azure subscription.</returns>
        [HttpGet]
        [Route("api/views/azure-subscriptions/{id}/display")]
        public async Task<IActionResult> GetDisplayViewModel(int id)
        {
            DisplayAzureSubscriptionVm result = await GetAsync<DisplayAzureSubscriptionVm>(null, $"/api/azure/subscriptions/{id}");
            List<DynamicResource> cost = await GetAsync<List<DynamicResource>>(_appServiceBaseUrls.Billing, $"/api/billing/costs/AzureSubscription/{result["SubscriptionId"]}");
            List<DynamicResource> managedResourceDeployments = await GetCollectionAsync<DynamicResource>(null, $"/api/azure/managed-resource-deployments?subscriptionId={id}");
            managedResourceDeployments.ForEach(mr => mr["Type"] = "azure");

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
            result["ManagedResourceDeployments"] = managedResourceDeployments;
            return Hypermedia(result);
        }

        /// <summary>
        /// Gets the template for a new subscription.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <returns>The new subscription template.</returns>
        [HttpGet]
        [Route("api/views/azure-subscriptions/add")]
        public async Task<IActionResult> GetAddViewModel(int tenantId, int cloudAccountId)
        {
            dynamic result = new AddSubscriptionVm { TenantId = tenantId, CloudAccountId = cloudAccountId };
            result.NewTemplate = await GetAsync<DynamicResource>(null, $"/api/azure/subscriptions/template?tenantId={tenantId}&cloudAccountId={cloudAccountId}");
            result.UnmanagedSubscriptions = await GetCollectionAsync<DynamicResource>(null, $"/api/azure/subscriptions/unmanaged?tenantId={tenantId}");

            // Set tenant to unmanaged subscription for potentially adding them
            foreach(dynamic subscription in result.UnmanagedSubscriptions)
            {
                subscription.TenantId = tenantId;
                subscription.CloudAccountId = cloudAccountId;
            }

            return Hypermedia(result);
        }

        /// <summary>
        /// Creates a new subscription.
        /// </summary>
        /// <returns>
        /// The link to the new subscription.
        /// </returns>
        [HttpPost]
        [Route("api/azure/subscriptions")]
        public async Task<IActionResult> Create()
        {
            string postResult = await ProxyPostAsync();
            int newEntityId = Convert.ToInt32(postResult);
            string newResourceUrl = Url.LinkTo<AzureSubscriptionViewsController>(c => c.GetDisplayViewModel(newEntityId));
            return Created(newResourceUrl, new string[] { newResourceUrl });
        }

        /// <summary>
        /// Cancels a subscription by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete]
        [Route("api/azure/subscriptions/{id}")]
        public Task<IActionResult> CancelById(int id)
        {
            return ProxyDeleteAsync();
        }
    }
}
