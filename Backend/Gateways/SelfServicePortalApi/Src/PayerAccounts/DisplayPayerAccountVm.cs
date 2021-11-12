using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.PayerAccounts
{
    /// <summary>
    /// View model to transport a payer account.
    /// </summary>
    public class DisplayPayerAccountVm : ResourceBase
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
    }
}
