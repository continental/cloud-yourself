namespace CloudYourself.Backend.AppServices.Billing.Dtos
{
    /// <summary>
    /// Data transfer object with summary data.
    /// </summary>
    public class SummaryDto
    {
        /// <summary>
        /// Gets or sets the payer accounts count.
        /// </summary>
        /// <value>
        /// The payer accounts count.
        /// </value>
        public int PayerAccountsCount { get; set; }

        /// <summary>
        /// Gets or sets the cost items count.
        /// </summary>
        /// <value>
        /// The cost items count.
        /// </value>
        public int CostItemsCount { get; set; }
    }
}
