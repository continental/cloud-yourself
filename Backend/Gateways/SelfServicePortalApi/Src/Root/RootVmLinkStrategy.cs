using CloudYourself.Backend.Gateways.SelfServicePortalApi.CloudAccounts;
using CloudYourself.Backend.Gateways.SelfServicePortalApi.Tenants;
using CloudYourself.Backend.Gateways.SelfServicePortalApi.Views.Home;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.Root
{
    /// <summary>
    /// Links the <see cref="RootVm"/> view model.
    /// </summary>
    /// <seealso cref="Fancy.ResourceLinker.LinkStrategyBase{CloudYourself.Backend.Gateways.SelfServicePortalApi.Root.RootVm}" />
    public class RootVmLinkStrategy : LinkStrategyBase<RootVm>
    {
        /// <summary>
        /// Links the resource internal.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(RootVm resource, IUrlHelper urlHelper)
        {
            resource.AddLink("self", urlHelper.LinkTo<RootController>(c => c.Get()));
            resource.AddLink("homeVm", urlHelper.LinkTo<HomeViewsController>(c => c.Get()));
            resource.AddLink("listTenantsVm", urlHelper.LinkTo<TenantViewsController>(c => c.GetListViewModel()));
            resource.AddLink("listCloudAccountsVm", urlHelper.LinkTo<CloudAccountViewsController>(c => c.GetListViewModel()));
        }
    }
}
