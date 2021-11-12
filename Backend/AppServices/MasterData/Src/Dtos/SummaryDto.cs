namespace CloudYourself.Backend.AppServices.MasterData.Dtos
{
    /// <summary>
    /// Data transfer object with summary data.
    /// </summary>
    public class SummaryDto
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
