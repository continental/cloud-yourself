using CloudYourself.Backend.AppServices.Billing.Aggregates.Cost;

namespace CloudYourself.Backend.AppServices.Billing.Dtos
{
    /// <summary>
    /// A data transfer object to create a new cost resource.
    /// </summary>
    public class NewCostDto
    {
        /// <summary>
        /// Gets or sets the type of the cost.
        /// </summary>
        /// <value>
        /// The type of the cost.
        /// </value>
        public CostType CostType { get; set; }

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

        /// <summary>
        /// Gets or sets the cost details.
        /// </summary>
        /// <value>
        /// The cost details.
        /// </value>
        public CostDetails CostDetails { get; set; } = new CostDetails();
    }
}
