namespace CloudYourself.Backend.AppServices.AzureSubscriptions.Dtos
{
    /// <summary>
    /// Dto to create information needed to create a new azure subscription.
    /// </summary>
    public class NewSubscriptionDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}
