namespace CloudYourself.Backend.AppServices.Billing.Aggregates.AllocationKey
{
    /// <summary>
    /// Value object with base data for an allocation key.
    /// </summary>
    public class AllocationKeyBaseData
    {
        /// <summary>
        /// Gets or sets the allocation percentage.
        /// </summary>
        /// <value>
        /// The allocation percentage.
        /// </value>
        public int AllocationPercentage { get; set; }
    }
}
