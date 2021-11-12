using CloudYourself.Backend.AppServices.Billing.Aggregates.Cost;

namespace CloudYourself.Backend.AppServices.Billing.Dtos
{
    /// <summary>
    /// Data transfer object to transport cost summary data of a payer account.
    /// </summary>
    public class CostSummaryPayerAccountDto
    {
        /// <summary>
        /// Gets or sets the payer account identifier.
        /// </summary>
        /// <value>
        /// The payer account identifier.
        /// </value>
        public int PayerAccountId { get; set; }

        /// <summary>
        /// Gets or sets the cost center.
        /// </summary>
        /// <value>
        /// The cost center.
        /// </value>
        public string CostCenter { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets the allocation amount sum.
        /// </summary>
        /// <value>
        /// The allocation amount sum.
        /// </value>
        public float AllocationAmountSum { get; set; }
    }
}
