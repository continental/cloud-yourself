using CloudYourself.Backend.Gateways.SelfServicePortalApi.AllocationKeys;
using CloudYourself.Backend.Gateways.SelfServicePortalApi.AwsAccounts;
using CloudYourself.Backend.Gateways.SelfServicePortalApi.AzureSubscriptions;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.CloudAccounts
{
    public class DisplayCloudAccountVmLinkStrategy : LinkStrategyBase<DisplayCloudAccountVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(DisplayCloudAccountVm resource, IUrlHelper urlHelper)
        {
            dynamic vm = resource;

            // Links
            resource.AddLink("self", urlHelper.LinkTo<CloudAccountViewsController>(c => c.GetDisplayViewModel(resource.Id)));
            resource.AddLink("editCloudAccountVm", urlHelper.LinkTo<CloudAccountViewsController>(c => c.GetEditViewModel(resource.Id)));
            resource.AddLink("addAzureSubscriptionVm", urlHelper.LinkTo<AzureSubscriptionViewsController>(c => c.GetAddViewModel(resource.TenantId, resource.Id)));
            resource.AddLink("addAwsAccountVm", urlHelper.LinkTo<AwsAccountViewsController>(c => c.GetAddViewModel(resource.TenantId, resource.Id)));
            resource.AddLink("createAllocationKeyVm", urlHelper.LinkTo<AllocationKeyViewsController>(c => c.GetCreateViewModel(resource.Id, resource.TenantId)));

            foreach(dynamic azureSubscription in vm.AzureSubscriptions)
            {
                int subscriptionId = Convert.ToInt32(azureSubscription.Id);
                azureSubscription.AddLink("displayAzureSubscriptionVm", urlHelper.LinkTo<AzureSubscriptionViewsController>(c => c.GetDisplayViewModel(subscriptionId)));
            }

            foreach (dynamic awsAccount in vm.AwsAccounts)
            {
                int accountId = Convert.ToInt32(awsAccount.Id);
                awsAccount.AddLink("displayAwsAccountVm", urlHelper.LinkTo<AwsAccountViewsController>(c => c.GetDisplayViewModel(accountId)));
            }

            foreach (dynamic allocationKey in vm.AllocationKeys)
            {
                int allocationKeyId = Convert.ToInt32(allocationKey.Id);
                allocationKey.AddLink("displayAllocationKeyVm", urlHelper.LinkTo<AllocationKeyViewsController>(c => c.GetDisplayViewModel(allocationKeyId)));
            }
        }
    }
}
