using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.AllocationKeys
{
    public class ListAllocationKeysVmLinkStrategy : LinkStrategyBase<ListAllocationKeysVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(ListAllocationKeysVm resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<AllocationKeyViewsController>(c => c.GetListViewModel(resource.CloudAccountId)));
            //resource.AddLink("createAllocationKeyVm", urlHelper.LinkTo<AllocationKeyViewsController>(c => c.GetCreateViewModel(resource.CloudAccountId)));

            foreach (dynamic allocationKey in resource.AllocationKeys)
            {
                int allocationKeyId = Convert.ToInt32(allocationKey.Id);
                allocationKey.AddLink("displayAllocationKeyVm", urlHelper.LinkTo<AllocationKeyViewsController>(c => c.GetDisplayViewModel(allocationKeyId)));
            }
        }
    }
}
