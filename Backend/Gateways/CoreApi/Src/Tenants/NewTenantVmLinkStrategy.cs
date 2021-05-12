using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.CoreApi.Tenants
{
    /// <summary>
    /// Link strategy to link a <see cref="NewTenantVm"/>.
    /// </summary>
    public class NewTenantVmLinkStrategy : LinkStrategyBase<NewTenantVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(NewTenantVm resource, IUrlHelper urlHelper)
        {
            dynamic vm = resource;

            // Links
            resource.AddLink("self", urlHelper.LinkTo<TenantsProxyController>(c => c.GetTemplate()));

            // Actions
            resource.AddAction("create", "POST", urlHelper.LinkTo<TenantsProxyController>(c => c.Create()));
        }
    }
}
