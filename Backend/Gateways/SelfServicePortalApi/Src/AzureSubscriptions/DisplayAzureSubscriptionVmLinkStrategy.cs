using CloudYourself.Backend.Gateways.SelfServicePortalApi.ManagedResourceDeployments;
using CloudYourself.Backend.Gateways.SelfServicePortalApi.ManagedResources;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.AzureSubscriptions
{
    public class DisplayAzureSubscriptionVmLinkStrategy : LinkStrategyBase<DisplayAzureSubscriptionVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(DisplayAzureSubscriptionVm resource, IUrlHelper urlHelper)
        {
            dynamic vm = resource;

            resource.AddLink("self", urlHelper.LinkTo<AzureSubscriptionViewsController>(c => c.GetDisplayViewModel(resource.Id)));
            resource.AddLink("listManagedResourcesVm", urlHelper.LinkTo<ManagedResourceViewsController>(c => c.GetListViewModel(resource.TenantId, "azure", resource.Id)));

            resource.AddSocket("self", "http://localhost:4100/hubs/azure-subscriptions", $"self({resource.Id})", null);

            if (vm.State == "Active")
            {
                resource.AddAction("cancel", "DELETE", urlHelper.LinkTo<AzureSubscriptionViewsController>(c => c.CancelById(resource.Id)));
            }

            if(resource.ContainsKey("ManagedResourceDeployments"))
            {
                foreach(dynamic managedResourceDeployment in vm.ManagedResourceDeployments)
                {
                    int managedResourceId = Convert.ToInt32(managedResourceDeployment.Id);
                    string managedResourceType = managedResourceDeployment["Type"] as string;
                    managedResourceDeployment.AddLink("displayManagedResourceDeploymentVm", urlHelper.LinkTo<ManagedResourceDeploymentsViewsController>(c => c.GetDisplayViewModel(managedResourceId, managedResourceType)));
                }
            }
        }
    }
}
