namespace CloudYourself.Backend.AppServices.Azure.Aggregates.ManagedResource
{
    /// <summary>
    /// Value object to hold base data of a managed resource.
    /// </summary>
    public class BaseData
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}
