using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.Gateways.CoreApi.Tenants
{
    /// <summary>
    /// View model to transport a tenant.
    /// </summary>
    public class TenantVm : ResourceBase
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
