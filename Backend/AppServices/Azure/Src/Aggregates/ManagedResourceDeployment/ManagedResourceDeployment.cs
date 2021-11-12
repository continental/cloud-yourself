using System;

namespace CloudYourself.Backend.AppServices.Azure.Aggregates.ManagedResourceDeployment
{
    /// <summary>
    /// An entity to hold information about a deployed managed resource.
    /// </summary>
    public class ManagedResourceDeployment
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public string Parameters { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; } = DateTime.Now.Date;

        /// <summary>
        /// Gets or sets the prepare date.
        /// </summary>
        /// <value>
        /// The prepare date.
        /// </value>
        public DateTime? PrepareDate { get; set; }

        /// <summary>
        /// Gets or sets the commit date.
        /// </summary>
        /// <value>
        /// The commit date.
        /// </value>
        public DateTime? CommitDate { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public ManagedResourceDeploymentState State { get; set; }
    }
}
