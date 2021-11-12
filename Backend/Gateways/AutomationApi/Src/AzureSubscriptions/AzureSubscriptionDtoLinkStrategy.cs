using CloudYourself.Backend.Gateways.AutomationApi.Costs;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.AutomationApi.AzureSubscriptions
{
    public class AzureSubscriptionDtoLinkStrategy : LinkStrategyBase<AzureSubscriptionDto>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(AzureSubscriptionDto resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<AzureSubscriptionsController>(c => c.GetById(resource.Id)));
            resource.AddLink("newCostTemplate", urlHelper.LinkTo<CostsController>(c => c.GetNewTemplate("AzureSubscription", resource.SubscriptionId, resource.CloudAccountId, resource.TenantId)));
            resource.AddLink("costs", urlHelper.LinkTo<CostsController>(c => c.GetByCostId("AzureSubscription", resource.SubscriptionId)));

            // Actions
            resource.AddAction("cancel", "DELETE", urlHelper.LinkTo<AzureSubscriptionsController>(c => c.CancelById(resource.Id)));
            resource.Compliance.AddAction("update", "PUT", urlHelper.LinkTo<AzureSubscriptionsController>(c => c.UpdateCompliance(resource.Id)));
        }
    }
}
