namespace CloudYourself.Backend.AppServices.Billing.Aggregates.Cost
{
    /// <summary>
    /// Enumerates the different cost types.
    /// </summary>
    public enum CostType
    {
        /// <summary>
        /// The unkown cost type.
        /// </summary>
        Unkown,

        /// <summary>
        /// The azure subscription cost type.
        /// </summary>
        AzureSubscription,

        /// <summary>
        /// The aws account cost type.
        /// </summary>
        AwsAccount
    }
}
