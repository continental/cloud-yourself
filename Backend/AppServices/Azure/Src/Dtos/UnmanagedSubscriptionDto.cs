using CloudYourself.Backend.AppServices.Azure.Aggregates.Subscription;

namespace CloudYourself.Backend.AppServices.Azure.Dtos
{
    /// <summary>
    /// Data transfer object to transport information about an existing but unmanaged subscription.
    /// </summary>
    public class UnmanagedSubscriptionDto
    {
        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        public int TenantId { get; set; }

        /// <summary>
        /// Gets or sets the cloud account identifier.
        /// </summary>
        /// <value>
        /// The cloud account identifier.
        /// </value>
        public int? CloudAccountId { get; set; }

        /// <summary>
        /// Gets or sets the subscription identifier.
        /// </summary>
        /// <value>
        /// The subscription identifier.
        /// </value>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the subscription link.
        /// </summary>
        /// <value>
        /// The subscription link.
        /// </value>
        public string SubscriptionLink { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set;  }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public SubscriptionState State { get; set; }
    }
}
