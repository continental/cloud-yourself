using CloudYourself.Backend.AppServices.Billing.Aggregates.AllocationKey;

namespace CloudYourself.Backend.AppServices.Billing.Dtos
{
    /// <summary>
    /// A data transfer object with data needed to create a new allocation key.
    /// </summary>
    public class NewAllocationKeyDto
    {
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
        public AllocationKeyBaseData BaseData { get; set; } = new AllocationKeyBaseData();
    }
}
