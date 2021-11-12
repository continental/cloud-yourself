using CloudYourself.Backend.AppServices.MasterData.Aggregates.Tenant;

namespace CloudYourself.Backend.AppServices.MasterData.Dtos
{
    /// <summary>
    /// Data transfer object with data needed to create a new tenant for cloud accounts.
    /// </summary>
    public class NewTenantDto
    {
        /// <summary>
        /// Gets or sets the base data.
        /// </summary>
        /// <value>
        /// The base data.
        /// </value>
        public TenantBaseData BaseData { get; set; }
    }
}
