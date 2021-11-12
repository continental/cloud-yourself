using CloudYourself.Backend.AppServices.Azure.Aggregates.ManagedResource;
using System.Collections.Generic;

namespace CloudYourself.Backend.AppServices.Azure.Dtos
{
    /// <summary>
    /// Dto with data needed to create a new azure managed resource deployment.
    /// </summary>
    public class NewManagedResourceDeploymentDto
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the deployment parameters.
        /// </summary>
        /// <value>
        /// The deployment parameters.
        /// </value>
        public List<DeploymentParamMetaDto> DeployParams { get; set; }
    }
}
