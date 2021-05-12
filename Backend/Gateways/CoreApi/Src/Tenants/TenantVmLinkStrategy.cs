using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.CoreApi.Tenants
{
    /// <summary>
    /// Link strategy to link a <see cref="TenantVm"/>.
    /// </summary>
    public class TenantVmLinkStrategy : LinkStrategyBase<TenantVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(TenantVm resource, IUrlHelper urlHelper)
        {
            dynamic vm = resource;

            // Links
            resource.AddLink("self", urlHelper.LinkTo<TenantsProxyController>(c => c.GetById(resource.Id)));

            // Actions
            vm.BaseData.AddAction("update", "PUT", urlHelper.LinkTo<TenantsProxyController>(c => c.UpdateBaseData(resource.Id)));

            if (resource.ContainsKey("AzureAppRegistration"))
            {
                vm.AzureAppRegistration.AddAction("update", "PUT", urlHelper.LinkTo<TenantsProxyController>(c => c.UpdateAzureSubsciptionsAppRegistration(resource.Id)));
            }

            if (resource.ContainsKey("AzureManagementTarget"))
            {
                vm.AzureManagementTarget.AddAction("update", "PUT", urlHelper.LinkTo<TenantsProxyController>(c => c.UpdateAzureSubsciptionsManagementTarget(resource.Id)));
            }
        }
    }
}
