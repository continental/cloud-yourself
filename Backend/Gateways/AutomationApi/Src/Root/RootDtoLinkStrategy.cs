using CloudYourself.Backend.Gateways.AutomationApi.CloudAccounts;
using CloudYourself.Backend.Gateways.AutomationApi.Tenants;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.AutomationApi.Root
{
    /// <summary>
    /// Links the <see cref="RootDto"/> view model.
    /// </summary>
    /// <seealso cref="Fancy.ResourceLinker.LinkStrategyBase{CloudYourself.Backend.Gateways.AutomationApi.Root.RootDto}" />
    public class RootDtoLinkStrategy : LinkStrategyBase<RootDto>
    {
        /// <summary>
        /// Links the resource internal.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(RootDto resource, IUrlHelper urlHelper)
        {
            resource.AddLink("tenants", urlHelper.LinkTo<TenantsController>(c => c.GetAll()));
            resource.AddLink("cloudAccounts", urlHelper.LinkTo<CloudAccountsController>(c => c.GetAll()));
        }
    }
}
