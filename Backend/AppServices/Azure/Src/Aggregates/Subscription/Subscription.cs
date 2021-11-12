namespace CloudYourself.Backend.AppServices.Azure.Aggregates.Subscription
{
    /// <summary>
    /// An entity that contains data for an azure subscription.
    /// </summary>
    public class Subscription
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
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        public int TenantId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public SubscriptionState State { get; set; }

        /// <summary>
        /// Gets or sets the creation operation URL.
        /// </summary>
        /// <value>
        /// The creation operation URL.
        /// </value>
        public string CreationOperationUrl { get; set; }

        /// <summary>
        /// Gets or sets the subscription link.
        /// </summary>
        /// <value>
        /// The subscription link.
        /// </value>
        public string SubscriptionLink { get; set; }

        /// <summary>
        /// Gets or sets the subscription identifier.
        /// </summary>
        /// <value>
        /// The subscription identifier.
        /// </value>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the compliance data about the subscription.
        /// </summary>
        /// <value>
        /// The compliance data.
        /// </value>
        public Compliance Compliance { get; set; } = new Compliance();
    }
}
