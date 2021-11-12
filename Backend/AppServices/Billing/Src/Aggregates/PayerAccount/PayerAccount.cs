using System.ComponentModel.DataAnnotations.Schema;

namespace CloudYourself.Backend.AppServices.Billing.Aggregates.PayerAccount
{
    /// <summary>
    /// An object to hold data of payer account.
    /// </summary>
    public class PayerAccount
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the tenant identifier the payer account belongs to.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        public int TenantId { get; set; }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        [NotMapped]
        public string Label => BaseData?.CostCenter + " - " + BaseData?.ProfitCenter;

        /// <summary>
        /// Gets or sets the base data.
        /// </summary>
        /// <value>
        /// The base data.
        /// </value>
        public PayerAccountBaseData BaseData { get; set; } = new PayerAccountBaseData();
    }
}
