using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.PayerAccounts
{
    /// <summary>
    /// Link strategy to link a <see cref="EditPayerAccountVm"/>.
    /// </summary>
    public class EditPayerAccountVmLinkStrategy : LinkStrategyBase<EditPayerAccountVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(EditPayerAccountVm resource, IUrlHelper urlHelper)
        {
            dynamic vm = resource;

            // Links
            resource.AddLink("self", urlHelper.LinkTo<PayerAccountViewsController>(c => c.GetEditViewModel(resource.Id)));

            // Actions
            vm.BaseData.AddAction("update", "PUT", urlHelper.LinkTo<PayerAccountViewsController>(c => c.UpdateBaseData(resource.Id)));
        }
    }
}
