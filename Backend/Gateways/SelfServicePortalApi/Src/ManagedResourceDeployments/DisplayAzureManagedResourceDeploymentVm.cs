using Fancy.ResourceLinker.Models;
using System.Collections.Generic;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.ManagedResourceDeployments
{
    /// <summary>
    /// View model to transport data needed to display an azure managed resource deployment.
    /// </summary>
    public class DisplayAzureManagedResourceDeploymentVm : ResourceBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the managed resource identifier.
        /// </summary>
        /// <value>
        /// The managed resource identifier.
        /// </value>
        public int ManagedResourceId { get; set; }

        /// <summary>
        /// Gets or sets the subscription identifier.
        /// </summary>
        /// <value>
        /// The subscription identifier.
        /// </value>
        public int SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the managed resource.
        /// </summary>
        /// <value>
        /// The managed resource.
        /// </value>
        public DynamicResource ManagedResource { get; set; }

        /// <summary>
        /// Gets or sets the deploy parameters.
        /// </summary>
        /// <value>
        /// The deploy parameters.
        /// </value>
        public List<DynamicResource> DeployParams { get; set; }
    }
}
