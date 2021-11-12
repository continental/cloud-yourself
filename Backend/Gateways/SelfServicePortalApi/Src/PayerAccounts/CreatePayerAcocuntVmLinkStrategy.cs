using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.PayerAccounts
{
    /// <summary>
    /// Link strategy to link a <see cref="CreatePayerAccountVm"/>.
    /// </summary>
    public class CreatePayerAcocuntVmLinkStrategy : LinkStrategyBase<CreatePayerAccountVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(CreatePayerAccountVm resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<PayerAccountViewsController>(c => c.GetCreateViewModel(resource.TenantId)));

            // Actions
            resource.AddAction("create", "POST", urlHelper.LinkTo<PayerAccountViewsController>(c => c.Create()));
        }
    }
}
