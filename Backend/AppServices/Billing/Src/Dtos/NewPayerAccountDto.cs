using CloudYourself.Backend.AppServices.Billing.Aggregates.PayerAccount;

namespace CloudYourself.Backend.AppServices.Billing.Dtos
{
    /// <summary>
    /// A data transfer object to create a new payer account.
    /// </summary>
    public class NewPayerAccountDto
    {
        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        public int TenantId { get; set; }

        /// <summary>
        /// Gets or sets the base data.
        /// </summary>
        /// <value>
        /// The base data.
        /// </value>
        public PayerAccountBaseData BaseData { get; set; } = new PayerAccountBaseData();
    }
}
