namespace CloudYourself.Backend.AppServices.Azure.Aggregates.Subscription
{
    /// <summary>
    /// Enumerates subscription states.
    /// </summary>
    public enum SubscriptionState
    {
        /// <summary>
        /// The pending state.
        /// </summary>
        Pending,

        /// <summary>
        /// The active state.
        /// </summary>
        Active,

        /// <summary>
        /// The cancelled state.
        /// </summary>
        Cancelled
    }
}
