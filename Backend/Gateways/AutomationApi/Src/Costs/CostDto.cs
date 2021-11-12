using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.Gateways.AutomationApi.Costs
{
    /// <summary>
    /// A view model to transport a cost.
    /// </summary>
    /// <seealso cref="Fancy.ResourceLinker.Models.ResourceBase" />
    public class CostDto : ResourceBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
    }
}
