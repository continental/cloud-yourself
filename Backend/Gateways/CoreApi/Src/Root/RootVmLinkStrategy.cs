using CloudYourself.Backend.Gateways.CoreApi.CloudAccounts;
using CloudYourself.Backend.Gateways.CoreApi.Home;
using CloudYourself.Backend.Gateways.CoreApi.Tenants;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.CoreApi.Root
{
    /// <summary>
    /// Links the <see cref="RootVm"/> view model.
    /// </summary>
    /// <seealso cref="Fancy.ResourceLinker.LinkStrategyBase{CloudYourself.Backend.Gateways.CoreApi.Root.RootVm}" />
    public class RootVmLinkStrategy : LinkStrategyBase<RootVm>
    {
        /// <summary>
        /// Links the resource internal.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(RootVm resource, IUrlHelper urlHelper)
        {
            resource.AddLink("tenants", urlHelper.LinkTo<TenantsProxyController>(c => c.GetAll()));
            resource.AddLink("cloudAccounts", urlHelper.LinkTo<CloudAccountsProxyController>(c => c.GetAll()));
            resource.AddLink("home", urlHelper.LinkTo<HomeController>(c => c.Get()));
        }
    }
}
