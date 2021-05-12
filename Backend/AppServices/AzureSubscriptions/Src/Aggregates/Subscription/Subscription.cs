
namespace CloudYourself.Backend.AppServices.AzureSubscriptions.Aggregates.Subscription
{
    /// <summary>
    /// Contains data for an azure subscription.
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
        /// Gets or sets the unique identifier of azure for this subscription.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public string Guid { get; set; }

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
    }
}
