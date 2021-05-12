namespace CloudYourself.Backend.Gateways.CoreApi.Infrastructure
{
    /// <summary>
    /// Options for app services base urls
    /// </summary>
    public class AppServiceBaseUrlOptions
    {
        /// <summary>
        /// The section name of this options group.
        /// </summary>
        public const string SectionName = "AppServiceBaseUrls";

        /// <summary>
        /// Gets or sets the cloud accounts.
        /// </summary>
        /// <value>
        /// The cloud accounts.
        /// </value>
        public string CloudAccounts { get; set; }

        /// <summary>
        /// Gets or sets the azure subscriptions.
        /// </summary>
        /// <value>
        /// The azure subscriptions.
        /// </value>
        public string AzureSubscriptions { get; set; }
    }
}
