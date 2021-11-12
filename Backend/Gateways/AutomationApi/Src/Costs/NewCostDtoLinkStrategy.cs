using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.AutomationApi.Costs
{
    /// <summary>
    /// Link strategy to link a <see cref="NewCostDto"/>.
    /// </summary>
    public class NewCostDtoLinkStrategy : LinkStrategyBase<NewCostDto>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(NewCostDto resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<CostsController>(c => c.GetNewTemplate(resource.CostType, resource.CostId, resource.CloudAccountId, resource.TenantId)));

            // Actions
            resource.AddAction("create", "POST", urlHelper.LinkTo<CostsController>(c => c.CreateNew()));
        }
    }
}
