namespace CloudYourself.Backend.AppServices.Azure.Aggregates.Subscription
{
    /// <summary>
    /// Enumeration to list different possible subscription compliance states.
    /// </summary>
    public enum ComplianceState
    {
        /// <summary>
        /// The unknown state.
        /// </summary>
        Unknown,

        /// <summary>
        /// The evaluating state.
        /// </summary>
        Evaluating,

        /// <summary>
        /// The compliant state.
        /// </summary>
        Compliant,

        /// <summary>
        /// The non compliant state.
        /// </summary>
        NonCompliant
    }
}
