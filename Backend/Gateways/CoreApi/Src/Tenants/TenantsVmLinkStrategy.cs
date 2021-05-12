using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.CoreApi.Tenants
{
    /// <summary>
    /// Link strategy to link a <see cref="TenantsVm"/>.
    /// </summary>
    public class TenantsVmLinkStrategy : LinkStrategyBase<TenantsVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(TenantsVm resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<TenantsProxyController>(c => c.GetAll()));
            resource.AddLink("template", urlHelper.LinkTo<TenantsProxyController>(c => c.GetTemplate()));
        }
    }
}
