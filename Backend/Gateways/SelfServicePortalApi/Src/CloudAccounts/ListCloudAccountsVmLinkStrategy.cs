using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.CloudAccounts
{
    public class ListCloudAccountsVmLinkStrategy : LinkStrategyBase<ListCloudAccountsVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(ListCloudAccountsVm resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<CloudAccountViewsController>(c => c.GetListViewModel()));
            resource.AddLink("requestCloudAccountVm", urlHelper.LinkTo<CloudAccountViewsController>(c => c.GetRequestViewModel()));

            foreach(dynamic cloudAccount in resource.CloudAccounts)
            {
                int cloudAccountId = Convert.ToInt32(cloudAccount.Id);
                cloudAccount.AddLink("displayCloundAccountVm", urlHelper.LinkTo<CloudAccountViewsController>(c => c.GetDisplayViewModel(cloudAccountId)));
            }
        }
    }
}
