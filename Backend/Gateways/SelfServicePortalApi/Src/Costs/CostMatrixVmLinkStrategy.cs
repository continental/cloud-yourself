using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.Costs
{
    /// <summary>
    /// Link strategy to link a <see cref="CostMatrixVm"/>.
    /// </summary>
    public class CostMatrixVmLinkStrategy : LinkStrategyBase<CostMatrixVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(CostMatrixVm resource, IUrlHelper urlHelper)
        {
            resource.AddLink("self", urlHelper.LinkTo<CostViewsController>(c => c.GetCostMatrixViewModel(resource.TenantId)));

            foreach(dynamic payerAccountCost in resource.PayerAccountCosts)
            {
                int payerAccountId = Convert.ToInt32(payerAccountCost.PayerAccountId);
                string currency = Convert.ToString(payerAccountCost.Currency);

                payerAccountCost.AddLink("cloudAccountCosts", urlHelper.LinkTo<CostViewsController>(c => c.GetCloudAccountCosts(payerAccountId, currency)));
            }
        }
    }
}
