using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.Gateways.CoreApi.Home
{
    /// <summary>
    /// View Model for infos shown on the home view.
    /// </summary>
    /// <seealso cref="Fancy.ResourceLinker.Models.ResourceBase" />
    public class HomeVm : ResourceBase
    {
        /// <summary>
        /// Gets or sets the dashboard infos.
        /// </summary>
        /// <value>
        /// The dashboard infos.
        /// </value>
        public DynamicResource DashboardInfos { get; set; }
    }
}
