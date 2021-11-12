using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.PayerAccounts
{
    /// <summary>
    /// View model to transport data needed to create a new payer account.
    /// </summary>
    public class CreatePayerAccountVm : ResourceBase
    {
        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        public int TenantId { get; set; }
    }
}
