using Fancy.ResourceLinker;
using Fancy.ResourceLinker.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.Tenants
{
    /// <summary>
    /// Link strategy to link a <see cref="ListTenantsVm"/>.
    /// </summary>
    public class ListTenantsVmLinkStrategy : LinkStrategyBase<ListTenantsVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(ListTenantsVm resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<TenantViewsController>(c => c.GetListViewModel()));
            resource.AddLink("createTenantVm", urlHelper.LinkTo<TenantViewsController>(c => c.GetCreateViewModel()));

            foreach(dynamic tenant in resource.Tenants)
            {
                int tenantId = Convert.ToInt32(tenant.Id);
                tenant.AddLink("displayTenantVm", urlHelper.LinkTo<TenantViewsController>(c => c.GetDisplayViewModel(tenantId)));
            }
        }
    }
}
