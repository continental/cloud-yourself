using Fancy.ResourceLinker.Models;
using System.Collections.Generic;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.CloudAccounts
{
    public class RequestCloudAccountVm : ResourceBase
    {
        /// <summary>
        /// Gets or sets the tenants.
        /// </summary>
        /// <value>
        /// The tenants.
        /// </value>
        public List<DynamicResource> Tenants { get; set; }

        /// <summary>
        /// Gets or sets the template for a new cloud account request.
        /// </summary>
        /// <value>
        /// The template.
        /// </value>
        public DynamicResource Template { get; set; }
    }
}
