using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.AppServices.CloudAccounts.Aggregates.Tenant
{
    /// <summary>
    /// Base data of a tenant.
    /// </summary>
    /// <seealso cref="Fancy.ResourceLinker.Models.ResourceBase" />
    public class TenantBaseData
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}
