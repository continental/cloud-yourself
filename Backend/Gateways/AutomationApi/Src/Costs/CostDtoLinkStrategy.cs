using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.AutomationApi.Costs
{
    /// <summary>
    /// Link strategy to link a <see cref="CostDto"/>.
    /// </summary>
    public class CostDtoLinkStrategy : LinkStrategyBase<CostDto>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(CostDto resource, IUrlHelper urlHelper)
        {
            dynamic vm = resource;

            // Links
            resource.AddLink("self", urlHelper.LinkTo<CostsController>(c => c.GetById(resource.Id)));

            // Actions
            vm.CostDetails.AddAction("update", "PUT", urlHelper.LinkTo<CostsController>(c => c.UpdateCostDetails(resource.Id)));
        }
    }
}
