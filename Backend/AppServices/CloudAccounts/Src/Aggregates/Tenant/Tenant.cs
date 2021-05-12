using Fancy.ResourceLinker.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CloudYourself.Backend.AppServices.CloudAccounts.Aggregates.Tenant
{
    /// <summary>
    /// A tenant of the system.
    /// </summary>
    /// <seealso cref="Fancy.ResourceLinker.Models.ResourceBase" />
    public class Tenant
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        [NotMapped]
        public string Label => BaseData?.Name;

        /// <summary>
        /// Gets or sets the base data.
        /// </summary>
        /// <value>
        /// The base data.
        /// </value>
        public TenantBaseData BaseData { get; set; }
    }
}
