using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.Gateways.AutomationApi.Costs
{
    /// <summary>
    /// A view model to transport a template for a new cost.
    /// </summary>
    /// <seealso cref="Fancy.ResourceLinker.Models.ResourceBase" />
    public class NewCostDto : ResourceBase
    {
        /// <summary>
        /// Gets or sets the type of the cost.
        /// </summary>
        /// <value>
        /// The type of the cost.
        /// </value>
        public string CostType { get; set; }

        /// <summary>
        /// Gets or sets the cost identifier.
        /// </summary>
        /// <value>
        /// The cost identifier.
        /// </value>
        public string CostId { get; set; }

        /// <summary>
        /// Gets or sets the cloud account identifier.
        /// </summary>
        /// <value>
        /// The cloud account identifier.
        /// </value>
        public int CloudAccountId { get; set; }

        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        public int TenantId { get; set; }
    }
}
