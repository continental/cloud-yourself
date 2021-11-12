using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.PayerAccounts
{
    public class ListPayerAccountsVmLinkStrategy : LinkStrategyBase<ListPayerAccountsVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(ListPayerAccountsVm resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<PayerAccountViewsController>(c => c.GetListViewModel(resource.TenantId)));
            resource.AddLink("createPayerAccountVm", urlHelper.LinkTo<PayerAccountViewsController>(c => c.GetCreateViewModel(resource.TenantId)));

            foreach (dynamic payerAccount in resource.PayerAccounts)
            {
                int payerAccountId = Convert.ToInt32(payerAccount.Id);
                payerAccount.AddLink("displayPayerAccountVm", urlHelper.LinkTo<PayerAccountViewsController>(c => c.GetDisplayViewModel(payerAccountId)));
            }
        }
    }
}
