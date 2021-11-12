using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.AzureSubscriptions
{
    public class AddSubscriptionVmLinkStrategy : LinkStrategyBase<AddSubscriptionVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(AddSubscriptionVm resource, IUrlHelper urlHelper)
        {
            dynamic vm = resource;

            vm.AddLink("self", urlHelper.LinkTo<AzureSubscriptionViewsController>(c => c.GetAddViewModel(resource.TenantId, resource.CloudAccountId)));

            vm.NewTemplate.AddAction("create", "POST", urlHelper.LinkTo<AzureSubscriptionViewsController>(c => c.Create()));

            foreach(dynamic unmanagedSubscription in vm.UnmanagedSubscriptions)
            {
                unmanagedSubscription.AddAction("add", "POST", urlHelper.LinkTo<AzureSubscriptionViewsController>(c => c.Create()));
            }
        }
    }
}
