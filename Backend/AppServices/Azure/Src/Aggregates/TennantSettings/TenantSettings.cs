namespace CloudYourself.Backend.AppServices.Azure.Aggregates.Tennant
{
    /// <summary>
    /// An entity to hold tenant related settings.
    /// </summary>
    public class TenantSettings
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the application registration settings.
        /// </summary>
        /// <value>
        /// The application registration settings.
        /// </value>
        public AppRegistration AppRegistration { get; set; } = new AppRegistration();

        /// <summary>
        /// Gets or sets the management target settings.
        /// </summary>
        /// <value>
        /// The management target settings.
        /// </value>
        public ManagementTarget ManagementTarget { get; set; } = new ManagementTarget();
    }
}
