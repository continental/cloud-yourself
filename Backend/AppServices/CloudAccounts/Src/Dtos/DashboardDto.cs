namespace CloudYourself.Backend.AppServices.CloudAccounts.Dtos
{
    /// <summary>
    /// Data transfer object with dashobard data.
    /// </summary>
    public class DashboardDto
    {
        /// <summary>
        /// Gets or sets the tenant count.
        /// </summary>
        /// <value>
        /// The tenant count.
        /// </value>
        public int TenantCount { get; set; }

        /// <summary>
        /// Gets or sets the cloud account count.
        /// </summary>
        /// <value>
        /// The cloud account count.
        /// </value>
        public int CloudAccountCount { get; set; }
    }
}
