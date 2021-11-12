using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.AppServices.MasterData.Aggregates.CloudAccount
{
    /// <summary>
    /// A cloud account.
    /// </summary>
    public class CloudAccount
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
        public int? TenantId { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public CloudAccountState State { get; set; }

        /// <summary>
        /// Gets or sets the base data.
        /// </summary>
        /// <value>
        /// The base data.
        /// </value>
        public CloudAccountBaseData BaseData { get; set; }
    }
}
