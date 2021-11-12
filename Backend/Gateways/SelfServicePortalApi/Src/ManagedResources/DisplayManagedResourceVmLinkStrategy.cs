using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.ManagedResources
{
    /// <summary>
    /// Link strategy to link a <see cref="DisplayManagedResourceVm"/>.
    /// </summary>
    public class DisplayManagedResourceVmLinkStrategy : LinkStrategyBase<DisplayManagedResourceVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(DisplayManagedResourceVm resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<ManagedResourceViewsController>(c => c.GetDisplayViewModel(resource.Id, resource.Type)));
            resource.AddLink("editManagedResourceVm", urlHelper.LinkTo<ManagedResourceViewsController>(c => c.GetEditViewModel(resource.Id, resource.Type)));
            resource.AddLink("listManagedResourcesVm", urlHelper.LinkTo<ManagedResourceViewsController>(c => c.GetListViewModel(resource.TenantId, null, null)));

            // Actions
            if (resource.Type == "azure")
            {
                resource.AddAction("delete", "DELETE", urlHelper.LinkTo<ManagedResourceViewsController>(c => c.DeleteAzureManagedResourceById(resource.Id)));
            }
        }
    }
}
