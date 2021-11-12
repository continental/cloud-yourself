using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.Gateways.AutomationApi.Tenants
{
    /// <summary>
    /// View model to transport a tenant.
    /// </summary>
    public class TenantDto : ResourceBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
    }
}
