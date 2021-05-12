namespace CloudYourself.Backend.AppServices.AzureSubscriptions.Aggregates.Tennant
{
    /// <summary>
    /// Contains infos about the target management group and enrollment account.
    /// </summary>
    public class ManagementTarget
    {
        /// <summary>
        /// Gets or sets the enrollment account identifier.
        /// </summary>
        /// <value>
        /// The enrollment account identifier.
        /// </value>
        public string EnrollmentAccountId { get; set; }

        /// <summary>
        /// Gets or sets the management group identifier.
        /// </summary>
        /// <value>
        /// The management group identifier.
        /// </value>
        public string ManagementGroupId { get; set; }
    }
}
