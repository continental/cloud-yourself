using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.CoreApi.CloudAccounts
{
    public class CloudAccountsVmLinkStrategy : LinkStrategyBase<CloudAccountsVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(CloudAccountsVm resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<CloudAccountsProxyController>(c => c.GetAll()));
            resource.AddLink("request", urlHelper.LinkTo<CloudAccountsProxyController>(c => c.GetRequest()));
        }
    }
}
