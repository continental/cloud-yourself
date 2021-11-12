using Fancy.ResourceLinker.Models;
using System.Collections.Generic;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.CloudAccounts
{
    /// <summary>
    /// View model to transport a list of cloud accounts.
    /// </summary>
    public class ListCloudAccountsVm : ResourceBase
    {
        /// <summary>
        /// Gets or sets the cloud accounts.
        /// </summary>
        /// <value>
        /// The cloud accounts.
        /// </value>
        public List<DynamicResource> CloudAccounts { get; set; }
    }
}
