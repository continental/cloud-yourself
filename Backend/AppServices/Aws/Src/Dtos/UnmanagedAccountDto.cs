
namespace CloudYourself.Backend.AppServices.Aws.Dtos
{
    /// <summary>
    /// Data transfer object to transport information about an existing but unmanaged subscription.
    /// </summary>
    public class UnmanagedAccountDto
    {
        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        public int TenantId { get; set; }

        /// <summary>
        /// Gets or sets the cloud account identifier.
        /// </summary>
        /// <value>
        /// The cloud account identifier.
        /// </value>
        public int? CloudAccountId { get; set; }

        /// <summary>
        /// Gets or sets the aws account identifier.
        /// </summary>
        /// <value>
        /// The aws account identifier.
        /// </value>
        public string AwsAccountId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set;  }
    }
}
