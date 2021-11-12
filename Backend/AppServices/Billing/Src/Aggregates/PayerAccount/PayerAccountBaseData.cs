namespace CloudYourself.Backend.AppServices.Billing.Aggregates.PayerAccount
{
    /// <summary>
    /// A value object with the base data of a payer account.
    /// </summary>
    public class PayerAccountBaseData
    {
        /// <summary>
        /// Gets or sets the cost center.
        /// </summary>
        /// <value>
        /// The cost center.
        /// </value>
        public string CostCenter { get; set; }

        /// <summary>
        /// Gets or sets the profit center.
        /// </summary>
        /// <value>
        /// The profit center.
        /// </value>
        public string ProfitCenter { get; set; }

        /// <summary>
        /// Gets or sets the name of the cost center responsible principal.
        /// </summary>
        /// <value>
        /// The name of the cost center responsible principal.
        /// </value>
        public string CostCenterResponsiblePrincipalName { get; set; }

        /// <summary>
        /// Gets or sets the name of the conntrolling contact principal.
        /// </summary>
        /// <value>
        /// The name of the conntrolling contact principal.
        /// </value>
        public string ControllingContactPrincipalName { get; set; }
    }
}
