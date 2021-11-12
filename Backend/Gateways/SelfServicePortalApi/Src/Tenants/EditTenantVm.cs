using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.Tenants
{
    /// <summary>
    /// View model to transport a tenant.
    /// </summary>
    public class EditTenantVm : ResourceBase
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
