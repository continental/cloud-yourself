using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.Costs
{
    /// <summary>
    /// Link strategy to link a <see cref="CloudAccountCostDto"/>.
    /// </summary>
    public class CloudAccountCostDtoLinkStrategy : LinkStrategyBase<CloudAccountCostDto>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(CloudAccountCostDto resource, IUrlHelper urlHelper)
        {
            dynamic dto = resource;
            
            int payerAccountId = Convert.ToInt32(dto.PayerAccountId);
            int cloudAccountId = Convert.ToInt32(dto.CloudAccountId);
            string currency = Convert.ToString(dto.Currency);

            resource.AddLink("costs", urlHelper.LinkTo<CostViewsController>(c => c.GetCosts(payerAccountId, cloudAccountId, currency)));
        }
    }
}
