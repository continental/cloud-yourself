namespace CloudYourself.Backend.AppServices.Azure.Aggregates.ManagedResourceDeployment
{
    /// <summary>
    /// Enumeration of possible managed resource deployment states.
    /// </summary>
    public enum ManagedResourceDeploymentState
    {
        /// <summary>
        /// The created state.
        /// </summary>
        Created,

        /// <summary>
        /// The preparing state.
        /// </summary>
        Preparing,

        /// <summary>
        /// The commited state.
        /// </summary>
        Commited
    }
}
