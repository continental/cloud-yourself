namespace CloudYourself.Backend.AppServices.AzureSubscriptions.Aggregates.Tennant
{
    /// <summary>
    /// Contains infos about the app registration to use. 
    /// </summary>
    public class AppRegistration
    {
        /// <summary>
        /// Gets or sets the azure directory tenant identifier.
        /// </summary>
        /// <value>
        /// The azure directory tenant identifier.
        /// </value>
        public string AzureDirectoryTenantId { get; set; }

        /// <summary>
        /// Gets or sets the azure application registration identifier.
        /// </summary>
        /// <value>
        /// The azure application registration identifier.
        /// </value>
        public string AzureAppRegistrationId { get; set; }

        /// <summary>
        /// Gets or sets the azure application secret.
        /// </summary>
        /// <value>
        /// The azure application secret.
        /// </value>
        public string AzureAppSecret { get; set; }
    }
}
