using CloudYourself.Backend.AppServices.Billing.Aggregates.Cost;

namespace CloudYourself.Backend.AppServices.Billing.Dtos
{
    /// <summary>
    /// Data transfer object to transport costs data which are owned
    /// </summary>
    public class CostPerAllocationKeyDto
    {
        /// <summary>
        /// Gets or sets the cloud account identifier.
        /// </summary>
        /// <value>
        /// The cloud account identifier.
        /// </value>
        public int CloudAccountId { get; set; }

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
        /// Gets or sets the period identifier.
        /// </summary>
        /// <value>
        /// The period identifier.
        /// </value>
        public string PeriodId { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets the total amount.
        /// </summary>
        /// <value>
        /// The total amount.
        /// </value>
        public float TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the allocation percentage.
        /// </summary>
        /// <value>
        /// The allocation percentage.
        /// </value>
        public float AllocationPercentage { get; set; }

        /// <summary>
        /// Gets or sets the allocation amount.
        /// </summary>
        /// <value>
        /// The allocation amount.
        /// </value>
        public float AllocationAmount { get; set; }
    }
}
