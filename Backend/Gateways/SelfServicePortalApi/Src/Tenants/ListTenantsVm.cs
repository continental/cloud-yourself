using Fancy.ResourceLinker.Models;
using System.Collections.Generic;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.Tenants
{
    /// <summary>
    /// View model for a list of tenants.
    /// </summary>
    /// <seealso cref="ResourceBase" />
    public class ListTenantsVm : ResourceBase
    {
        /// <summary>
        /// Gets or sets the tenants.
        /// </summary>
        /// <value>
        /// The tenants.
        /// </value>
        public List<DynamicResource> Tenants { get; set; }
    }
}
