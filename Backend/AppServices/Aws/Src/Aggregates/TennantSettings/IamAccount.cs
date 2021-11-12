namespace CloudYourself.Backend.AppServices.Aws.Aggregates.TennantSettings
{
    /// <summary>
    /// Contains infos about the necessary AWS IAM account to work with AWS apis.
    /// </summary>
    public class IamAccount
    {
        /// <summary>
        /// Gets or sets the access key identifier.
        /// </summary>
        /// <value>
        /// The access key identifier.
        /// </value>
        public string AccessKeyId { get; set; }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>
        /// The secret.
        /// </value>
        public string Secret { get; set; }
    }
}
