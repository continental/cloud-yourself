using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.AllocationKeys
{
    /// <summary>
    /// View model to transport an allocation key for the edit view.
    /// </summary>
    public class EditAllocationKeyVm : ResourceBase
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
