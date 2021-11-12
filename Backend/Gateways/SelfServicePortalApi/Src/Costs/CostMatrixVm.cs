using Fancy.ResourceLinker.Models;
using System.Collections.Generic;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.Costs
{
    /// <summary>
    /// View Model for infos shown on the cost matrix.
    /// </summary>
    /// <seealso cref="ResourceBase" />
    public class CostMatrixVm : ResourceBase
    {
        /// <summary>
        /// Gets or sets the tenant identifier the values the cost matrix apply to.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        public int TenantId { get; set; }

        /// <summary>
        /// Gets the payer account costs.
        /// </summary>
        /// <value>
        /// The payer account costs.
        /// </value>
        public List<DynamicResource> PayerAccountCosts { get; internal set; }
    }
}
