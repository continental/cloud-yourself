namespace CloudYourself.Backend.AppServices.AzureSubscriptions.Aggregates.Tennant
{
    /// <summary>
    /// A class to hold tenant related settings.
    /// </summary>
    public class Tenant
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the application registration.
        /// </summary>
        /// <value>
        /// The application registration.
        /// </value>
        public AppRegistration AppRegistration { get; set; } = new AppRegistration();

        /// <summary>
        /// Gets or sets the management target.
        /// </summary>
        /// <value>
        /// The management target.
        /// </value>
        public ManagementTarget ManagementTarget { get; set; } = new ManagementTarget();
    }
}
