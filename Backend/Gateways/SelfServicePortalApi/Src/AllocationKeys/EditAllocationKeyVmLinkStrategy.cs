using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.AllocationKeys
{
    /// <summary>
    /// Link strategy to link a <see cref="EditAllocationKeyVm"/>.
    /// </summary>
    public class EditAllocationKeyVmLinkStrategy : LinkStrategyBase<EditAllocationKeyVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(EditAllocationKeyVm resource, IUrlHelper urlHelper)
        {
            dynamic vm = resource;

            // Links
            resource.AddLink("self", urlHelper.LinkTo<AllocationKeyViewsController>(c => c.GetEditViewModel(resource.Id)));

            // Actions
            vm.BaseData.AddAction("update", "PUT", urlHelper.LinkTo<AllocationKeyViewsController>(c => c.UpdateBaseData(resource.Id)));
        }
    }
}
