using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.ManagedResources
{
    /// <summary>
    /// Link strategy to link a <see cref="CreateManagedResourceVm"/>.
    /// </summary>
    public class CreateManagedResourceVmLinkStrategy : LinkStrategyBase<CreateManagedResourceVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(CreateManagedResourceVm resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<ManagedResourceViewsController>(c => c.GetCreateViewModel(resource.TenantId, resource.Type)));

            // Actions
            resource.AddAction("create", "POST", urlHelper.LinkTo<ManagedResourceViewsController>(c => c.Create(resource.Type)));
        }
    }
}
