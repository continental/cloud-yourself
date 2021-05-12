using Fancy.ResourceLinker.Models;
using System.Collections.Generic;

namespace CloudYourself.Backend.Gateways.CoreApi.Tenants
{
    /// <summary>
    /// View model for a list of tenants.
    /// </summary>
    /// <seealso cref="Fancy.ResourceLinker.Models.ResourceBase" />
    public class TenantsVm : ResourceBase
    {
        /// <summary>
        /// Gets or sets the tenants.
        /// </summary>
        /// <value>
        /// The tenants.
        /// </value>
        public List<TenantVm> Tenants { get; set; }
    }
}
