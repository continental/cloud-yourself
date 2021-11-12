using Fancy.ResourceLinker.Models;
using System.Collections.Generic;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.AllocationKeys
{
    /// <summary>
    /// View model for a list of allocation keys.
    /// </summary>
    /// <seealso cref="ResourceBase" />
    public class ListAllocationKeysVm : ResourceBase
    {
        /// <summary>
        /// Gets or sets the cloud account identifier.
        /// </summary>
        /// <value>
        /// The cloud account identifier.
        /// </value>
        public int CloudAccountId { get; set; }

        /// <summary>
        /// Gets or sets the allocation keys.
        /// </summary>
        /// <value>
        /// The allocation keys.
        /// </value>
        public List<DynamicResource> AllocationKeys { get; set; }
    }
}
