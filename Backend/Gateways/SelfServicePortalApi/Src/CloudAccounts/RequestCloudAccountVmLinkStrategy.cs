using CloudYourself.Backend.Gateways.SelfServicePortalApi.AzureSubscriptions;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.CloudAccounts
{
    public class RequestCloudAccountVmLinkStrategy : LinkStrategyBase<RequestCloudAccountVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(RequestCloudAccountVm resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<CloudAccountViewsController>(c => c.GetRequestViewModel()));

            // Actions
            resource.Template.AddAction("create", "POST", urlHelper.LinkTo<CloudAccountViewsController>(c => c.CreateRequest()));
        }
    }
}
