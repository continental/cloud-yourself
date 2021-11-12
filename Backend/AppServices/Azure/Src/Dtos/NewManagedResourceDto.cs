using CloudYourself.Backend.AppServices.Azure.Aggregates.ManagedResource;

namespace CloudYourself.Backend.AppServices.Azure.Dtos
{
    /// <summary>
    /// Dto with data needed to create a new azure managed resource.
    /// </summary>
    public class NewManagedResourceDto
    {
        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        public int TenantId { get; set; }

        /// <summary>
        /// Gets or sets the base data.
        /// </summary>
        /// <value>
        /// The base data.
        /// </value>
        public BaseData BaseData { get; set; } = new BaseData();

        /// <summary>
        /// Gets or sets the compliance settings.
        /// </summary>
        /// <value>
        /// The compliance settings.
        /// </value>
        public ComplianceSettings ComplianceSettings { get; set; } = new ComplianceSettings();

        /// <summary>
        /// Gets or sets the arm template.
        /// </summary>
        /// <value>
        /// The arm template.
        /// </value>
        public ArmTemplate ArmTemplate { get; set; } = new ArmTemplate();
    }
}
