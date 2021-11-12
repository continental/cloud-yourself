using Fancy.ResourceLinker.Models;
using System.Collections.Generic;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.ManagedResources
{
    /// <summary>
    /// View model for a list of managed resources.
    /// </summary>
    /// <seealso cref="ResourceBase" />
    public class ListManagedResourcesVm : ResourceBase
    {
        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        public int TenantId { get; set; }

        /// <summary>
        /// Gets or sets the deployment subscription identifier.
        /// </summary>
        /// <value>
        /// The deployment subscription identifier.
        /// </value>
        public int? DeploymentSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the managed resources.
        /// </summary>
        /// <value>
        /// The managed resources.
        /// </value>
        public List<DynamicResource> ManagedResources { get; set; }
    }
}
