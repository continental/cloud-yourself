using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.Gateways.CoreApi.AzureSubscriptions
{
    /// <summary>
    /// View model for an azure subscription
    /// </summary>
    /// <seealso cref="Fancy.ResourceLinker.Models.ResourceBase" />
    public class AzureSubscriptionVm : ResourceBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set;  }

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
        public int CloudAccountId { get; set; }
    }
}
