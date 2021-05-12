using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.CoreApi.AzureSubscriptions
{
    public class NewSubscriptionVmLinkStrategy : LinkStrategyBase<NewSubscriptionVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(NewSubscriptionVm resource, IUrlHelper urlHelper)
        {
            resource.AddLink("self", urlHelper.LinkTo<AzureSubscriptionsProxyController>(c => c.GetNewTemplate(resource.TenantId, resource.CloudAccountId)));

            resource.AddAction("create", "POST", urlHelper.LinkTo<AzureSubscriptionsProxyController>(c => c.Create(resource.TenantId, resource.CloudAccountId)));
        }
    }
}
