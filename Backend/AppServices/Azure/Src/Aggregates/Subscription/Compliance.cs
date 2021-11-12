namespace CloudYourself.Backend.AppServices.Azure.Aggregates.Subscription
{
    /// <summary>
    /// Value object to hold compliance data about a subscription.
    /// </summary>
    public class Compliance
    {
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public ComplianceState State { get; set; }

        /// <summary>
        /// Gets or sets the policy evaluation result URL.
        /// </summary>
        /// <value>
        /// The policy evaluation result URL.
        /// </value>
        public string PolicyEvaluationResultUrl { get; set; }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            State = ComplianceState.Unknown;
            PolicyEvaluationResultUrl = null;
        }
    }
}
