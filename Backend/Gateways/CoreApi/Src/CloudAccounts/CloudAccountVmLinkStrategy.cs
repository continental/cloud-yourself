using CloudYourself.Backend.Gateways.CoreApi.AzureSubscriptions;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.CoreApi.CloudAccounts
{
    public class CloudAccountVmLinkStrategy : LinkStrategyBase<CloudAccountVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(CloudAccountVm resource, IUrlHelper urlHelper)
        {
            dynamic vm = resource;

            // Links
            resource.AddLink("self", urlHelper.LinkTo<CloudAccountsProxyController>(c => c.GetById(resource.Id)));
            resource.AddLink("azureSubscriptionTemplate", urlHelper.LinkTo<AzureSubscriptionsProxyController>(c => c.GetNewTemplate(resource.TenantId, resource.Id)));

            // Actions
            if(resource.State != "Approved")
            {
                resource.AddAction("approve", "POST", urlHelper.LinkTo<CloudAccountsProxyController>(c => c.SetState("Approved")));
            }
            vm.BaseData.AddAction("update", "PUT", urlHelper.LinkTo<CloudAccountsProxyController>(c => c.UpdateBaseData(resource.Id)));
        }
    }
}
