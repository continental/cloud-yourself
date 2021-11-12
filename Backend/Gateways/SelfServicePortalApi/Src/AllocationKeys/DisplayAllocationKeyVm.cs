using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.AllocationKeys
{
    /// <summary>
    /// View model to transport an allocation key.
    /// </summary>
    public class DisplayAllocationKeyVm : ResourceBase
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
        /// Gets or sets the payer account identifier.
        /// </summary>
        /// <value>
        /// The payer account identifier.
        /// </value>
        public int PayerAccountId { get; set; }
    }
}
