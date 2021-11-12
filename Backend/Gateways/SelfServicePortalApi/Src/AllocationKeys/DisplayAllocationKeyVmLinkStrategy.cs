using CloudYourself.Backend.Gateways.SelfServicePortalApi.CloudAccounts;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.AllocationKeys
{
    /// <summary>
    /// Link strategy to link a <see cref="DisplayAllocationKeyVm"/>.
    /// </summary>
    public class DisplayAllocationKeyVmLinkStrategy : LinkStrategyBase<DisplayAllocationKeyVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(DisplayAllocationKeyVm resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<AllocationKeyViewsController>(c => c.GetDisplayViewModel(resource.Id)));
            resource.AddLink("editAllocationKeyVm", urlHelper.LinkTo<AllocationKeyViewsController>(c => c.GetEditViewModel(resource.Id)));
            resource.AddLink("displayCloudAccountVm", urlHelper.LinkTo<CloudAccountViewsController>(c => c.GetDisplayViewModel(resource.CloudAccountId)));

            // Actions
            resource.AddAction("delete", "DELETE", urlHelper.LinkTo<AllocationKeyViewsController>(c => c.DeleteById(resource.Id)));
        }
    }
}
