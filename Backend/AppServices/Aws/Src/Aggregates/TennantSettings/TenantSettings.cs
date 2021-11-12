namespace CloudYourself.Backend.AppServices.Aws.Aggregates.TennantSettings
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
        /// Gets or sets the iam account.
        /// </summary>
        /// <value>
        /// The iam account.
        /// </value>
        public IamAccount IamAccount { get; set; } = new IamAccount();
    }
}
