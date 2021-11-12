using CloudYourself.Backend.AppServices.MasterData.Aggregates.CloudAccount;

namespace CloudYourself.Backend.AppServices.MasterData.Dtos
{
    /// <summary>
    /// Data transfer object with data needed to create a new cloud account.
    /// </summary>
    public class CloudAccountRequestDto
    {
        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        public int TenantId { get; set; }

        /// <summary>
        /// Gets or sets the base data.
        /// </summary>
        /// <value>
        /// The base data.
        /// </value>
        public CloudAccountBaseData BaseData { get; set; }
    }
}
