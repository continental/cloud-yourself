using Fancy.ResourceLinker.Models;
using System.Collections.Generic;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.ManagedResourceDeployments
{
    /// <summary>
    /// View model to transport data needed to deploy a new azure managed resource.
    /// </summary>
    public class CreateAzureManagedResourceDeploymentVm : ResourceBase
    {
        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        public int TenantId { get; set; }

        /// <summary>
        /// Gets or sets the managed resource identifier.
        /// </summary>
        /// <value>
        /// The managed resource identifier.
        /// </value>
        public int ManagedResourceId { get; set; }

        /// <summary>
        /// Gets or sets the target subscription identifier.
        /// </summary>
        /// <value>
        /// The target subscription identifier.
        /// </value>
        public int TargetSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the managed resource.
        /// </summary>
        /// <value>
        /// The managed resource.
        /// </value>
        public DynamicResource BaseData { get; set; }

        /// <summary>
        /// Gets or sets the deploy parameters.
        /// </summary>
        /// <value>
        /// The deploy parameters.
        /// </value>
        public List<DynamicResource> DeployParams { get; set; }
    }
}
