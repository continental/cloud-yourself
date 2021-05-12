using Fancy.ResourceLinker.Models;
using System.Collections.Generic;

namespace CloudYourself.Backend.Gateways.CoreApi.CloudAccounts
{
    /// <summary>
    /// View model to transport a list of cloud accounts.
    /// </summary>
    public class CloudAccountsVm : ResourceBase
    {
        /// <summary>
        /// Gets or sets the cloud accounts.
        /// </summary>
        /// <value>
        /// The cloud accounts.
        /// </value>
        public List<CloudAccountVm> CloudAccounts { get; set; }
    }
}
