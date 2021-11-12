using CloudYourself.Backend.Gateways.SelfServicePortalApi.AzureSubscriptions;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.CloudAccounts
{
    public class EditCloudAccountVmLinkStrategy : LinkStrategyBase<EditCloudAccountVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(EditCloudAccountVm resource, IUrlHelper urlHelper)
        {
            dynamic vm = resource;

            // Links
            resource.AddLink("self", urlHelper.LinkTo<CloudAccountViewsController>(c => c.GetDisplayViewModel(resource.Id)));

            vm.BaseData.AddAction("update", "PUT", urlHelper.LinkTo<CloudAccountViewsController>(c => c.UpdateBaseData(resource.Id)));
        }
    }
}
