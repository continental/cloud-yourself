using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.AllocationKeys
{
    /// <summary>
    /// Link strategy to link a <see cref="CreateAllocationKeyVm"/>.
    /// </summary>
    public class CreateAllocationKeyVmLinkStrategy : LinkStrategyBase<CreateAllocationKeyVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(CreateAllocationKeyVm resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<AllocationKeyViewsController>(c => c.GetCreateViewModel(resource.CloudAccountId, resource.TenantId)));

            // Actions
            resource.AddAction("create", "POST", urlHelper.LinkTo<AllocationKeyViewsController>(c => c.Create()));
        }
    }
}
