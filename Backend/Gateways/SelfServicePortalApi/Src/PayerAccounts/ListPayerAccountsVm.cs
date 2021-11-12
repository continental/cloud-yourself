using Fancy.ResourceLinker.Models;
using System.Collections.Generic;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.PayerAccounts
{
    /// <summary>
    /// View model for a list of payer accounts.
    /// </summary>
    /// <seealso cref="ResourceBase" />
    public class ListPayerAccountsVm : ResourceBase
    {
        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        public int TenantId { get; set; }

        /// <summary>
        /// Gets or sets the payer accounts.
        /// </summary>
        /// <value>
        /// The payer accounts.
        /// </value>
        public List<DynamicResource> PayerAccounts { get; set; }
    }
}
