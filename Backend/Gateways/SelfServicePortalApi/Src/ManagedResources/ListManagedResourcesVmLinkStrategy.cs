using CloudYourself.Backend.Gateways.SelfServicePortalApi.ManagedResourceDeployments;
using CloudYourself.Backend.Gateways.SelfServicePortalApi.ManagedResources;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.CloudAccounts
{
    public class ListManagedResourcesVmLinkStrategy : LinkStrategyBase<ListManagedResourcesVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(ListManagedResourcesVm resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<ManagedResourceViewsController>(c => c.GetListViewModel(resource.TenantId, null, null)));
            resource.AddLink("addAzureManagedResourceVm", urlHelper.LinkTo<ManagedResourceViewsController>(c => c.GetCreateViewModel(resource.TenantId, "azure")));

            foreach (dynamic managedResource in resource.ManagedResources)
            {
                int managedResourceId = Convert.ToInt32(managedResource.Id);
                string managedResourceType = managedResource.Type;
                managedResource.AddLink("displayManagedResourceVm", urlHelper.LinkTo<ManagedResourceViewsController>(c => c.GetDisplayViewModel(managedResourceId, managedResourceType)));

                if(managedResource.Type == "azure" && resource.DeploymentSubscriptionId.HasValue)
                {
                    managedResource.AddLink("createAzureManagedResourceDeploymentVm", urlHelper.LinkTo<ManagedResourceDeploymentsViewsController>(c => c.GetCreateViewModel(resource.DeploymentSubscriptionId.Value, managedResourceId, "azure")));
                }
            }
        }
    }
}
