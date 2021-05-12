using CloudYourself.Backend.Gateways.CoreApi.AzureSubscriptions;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.CoreApi.CloudAccounts
{
    public class CloudAccountRequestVmLinkStrategy : LinkStrategyBase<CloudAccountRequestVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(CloudAccountRequestVm resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<CloudAccountsProxyController>(c => c.GetRequest()));

            // Actions
            resource.Template.AddAction("create", "POST", urlHelper.LinkTo<CloudAccountsProxyController>(c => c.Create()));
        }
    }
}
