namespace CloudYourself.Backend.AppServices.Azure.Aggregates.ManagedResource
{
    /// <summary>
    /// Value object to hold ARM template of a managed resource.
    /// </summary>
    public class ArmTemplate
    {
        /// <summary>
        /// Gets or sets the code of the arm template.
        /// </summary>
        /// <value>
        /// The code of the arm template.
        /// </value>
        public string Template { get; set; }
    }
}
