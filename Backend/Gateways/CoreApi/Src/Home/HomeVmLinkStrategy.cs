using CloudYourself.Backend.Gateways.CoreApi.CloudAccounts;
using CloudYourself.Backend.Gateways.CoreApi.Tenants;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.CoreApi.Home
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
            resource.AddLink("self", urlHelper.LinkTo<HomeController>(c => c.Get()));
            resource.AddLink("tenants", urlHelper.LinkTo<TenantsProxyController>(c => c.GetAll()));
            resource.AddLink("cloudAccounts", urlHelper.LinkTo<CloudAccountsProxyController>(c => c.GetAll()));
        }
    }
}
