using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.Costs
{
    /// <summary>
    /// A data transfer object to transport the data of a cloud account cost.
    /// </summary>
    public class CloudAccountCostDto : ResourceBase
    {
        /// <summary>
        /// Gets or sets the cloud account identifier.
        /// </summary>
        /// <value>
        /// The cloud account identifier.
        /// </value>
        public int CloudAccountId { get; set; }
    }
}
