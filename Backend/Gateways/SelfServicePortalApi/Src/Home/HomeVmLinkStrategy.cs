using CloudYourself.Backend.Gateways.SelfServicePortalApi.CloudAccounts;
using CloudYourself.Backend.Gateways.SelfServicePortalApi.Tenants;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.Views.Home
{
    public class HomeVmLinkStrategy : LinkStrategyBase<HomeVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(HomeVm resource, IUrlHelper urlHelper)
        {
            resource.AddLink("self", urlHelper.LinkTo<HomeViewsController>(c => c.Get()));
            resource.AddLink("listTenantsVm", urlHelper.LinkTo<TenantViewsController>(c => c.GetListViewModel()));
            resource.AddLink("listCloudAccountsVm", urlHelper.LinkTo<CloudAccountViewsController>(c => c.GetListViewModel()));
        }
    }
}
