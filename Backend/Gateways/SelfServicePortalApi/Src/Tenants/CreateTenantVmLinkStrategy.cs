using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.Tenants
{
    /// <summary>
    /// Link strategy to link a <see cref="CreateTenantVm"/>.
    /// </summary>
    public class CreateTenantVmLinkStrategy : LinkStrategyBase<CreateTenantVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(CreateTenantVm resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<TenantViewsController>(c => c.GetCreateViewModel()));

            // Actions
            resource.AddAction("create", "POST", urlHelper.LinkTo<TenantViewsController>(c => c.Create()));
        }
    }
}
