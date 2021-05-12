using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.AppServices.CloudAccounts.Aggregates.CloudAccount
{
    /// <summary>
    /// Base data of a cloud account
    /// </summary>
    public class CloudAccountBaseData
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the division.
        /// </summary>
        /// <value>
        /// The division.
        /// </value>
        public string Division { get; set; }

        /// <summary>
        /// Gets or sets the legal entity.
        /// </summary>
        /// <value>
        /// The legal entity.
        /// </value>
        public string LegalEntity { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets the operational contact.
        /// </summary>
        /// <value>
        /// The operational contact.
        /// </value>
        public string OperationalContact { get; set; }
    }
}
