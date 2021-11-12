namespace CloudYourself.Backend.AppServices.Azure.Aggregates.ManagedResource
{
    /// <summary>
    /// Value object to hold compliance settings abount a managed resource.
    /// </summary>
    public class ComplianceSettings
    {
        /// <summary>
        /// Gets or sets the name of the initiative assignment.
        /// </summary>
        /// <value>
        /// The name of the initiative assignment.
        /// </value>
        public string InitiativeAssignmentName { get; set; }

        /// <summary>
        /// Gets or sets the initiative definition identifier.
        /// </summary>
        /// <value>
        /// The initiative definition identifier.
        /// </value>
        public string InitiativeDefinitionId { get; set; }
    }
}
