using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.CloudAccounts
{
    /// <summary>
    /// View model to transport a cloud account.
    /// </summary>
    public class EditCloudAccountVm : ResourceBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        public int TenantId { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }
    }
}
