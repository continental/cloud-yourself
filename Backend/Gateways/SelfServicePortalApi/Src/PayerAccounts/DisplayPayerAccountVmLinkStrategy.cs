using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.PayerAccounts
{
    /// <summary>
    /// Link strategy to link a <see cref="DisplayPayerAccountVm"/>.
    /// </summary>
    public class DisplayPayerAccountVmLinkStrategy : LinkStrategyBase<DisplayPayerAccountVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(DisplayPayerAccountVm resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<PayerAccountViewsController>(c => c.GetDisplayViewModel(resource.Id)));
            resource.AddLink("editPayerAccountVm", urlHelper.LinkTo<PayerAccountViewsController>(c => c.GetEditViewModel(resource.Id)));
            resource.AddLink("listPayerAccountsVm", urlHelper.LinkTo<PayerAccountViewsController>(c => c.GetListViewModel(resource.TenantId)));

            // Actions
            resource.AddAction("delete", "DELETE", urlHelper.LinkTo<PayerAccountViewsController>(c => c.DeleteById(resource.Id)));
        }
    }
}
