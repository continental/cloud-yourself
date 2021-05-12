using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.CoreApi.AzureSubscriptions
{
    public class AzureSubscriptionVmLinkStrategy : LinkStrategyBase<AzureSubscriptionVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(AzureSubscriptionVm resource, IUrlHelper urlHelper)
        {
            dynamic vm = resource;

            resource.AddLink("self", urlHelper.LinkTo<AzureSubscriptionsProxyController>(c => c.GetById(resource.TenantId, resource.CloudAccountId, resource.Id)));

            resource.AddSocket("self", "http://localhost:4100/hubs/azure-subscriptions", $"self({resource.Id})", null);

            if (vm.State == "Active")
            {
                resource.AddAction("cancel", "DELETE", urlHelper.LinkTo<AzureSubscriptionsProxyController>(c => c.CancelById(resource.TenantId, resource.CloudAccountId, resource.Id)));
            }
        }
    }
}
