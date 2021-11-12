namespace CloudYourself.Backend.AppServices.Billing.Aggregates.AllocationKey
{
    /// <summary>
    /// An allocation key to assign a payer account to a cloud account.
    /// </summary>
    public class AllocationKey
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the cloud account identifier.
        /// </summary>
        /// <value>
        /// The cloud account identifier.
        /// </value>
        public int CloudAccountId { get; set; }

        /// <summary>
        /// Gets or sets the payer account identifier.
        /// </summary>
        /// <value>
        /// The payer account identifier.
        /// </value>
        public int PayerAccountId { get; set; }

        /// <summary>
        /// Gets or sets the base data.
        /// </summary>
        /// <value>
        /// The base data.
        /// </value>
        public AllocationKeyBaseData BaseData { get; set; }
    }
}
