using CloudYourself.Backend.AppServices.CloudAccounts.Aggregates.Tenant;

namespace CloudYourself.Backend.AppServices.CloudAccounts.Dtos
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
