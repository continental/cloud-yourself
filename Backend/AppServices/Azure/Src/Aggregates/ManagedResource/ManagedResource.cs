
namespace CloudYourself.Backend.AppServices.Azure.Aggregates.ManagedResource
{
    /// <summary>
    /// An entity to hold information about managed resources.
    /// </summary>
    public class ManagedResource
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

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
