using CloudYourself.Backend.Gateways.SelfServicePortalApi.AzureSubscriptions;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.ManagedResourceDeployments
{
    /// <summary>
    /// Link strategy to link a <see cref="DeployAzureManagedResourceVm"/>.
    /// </summary>
    public class CreateAzureManagedResourceDeploymentVmLinkStrategy : LinkStrategyBase<CreateAzureManagedResourceDeploymentVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(CreateAzureManagedResourceDeploymentVm resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<ManagedResourceDeploymentsViewsController>(c => c.GetCreateViewModel(resource.TargetSubscriptionId, resource.ManagedResourceId, "azure")));
            resource.AddLink("subscription", urlHelper.LinkTo<AzureSubscriptionViewsController>(c => c.GetDisplayViewModel(resource.TargetSubscriptionId)));

            // Actions
            resource.AddAction("create", "POST", urlHelper.LinkTo<ManagedResourceDeploymentsViewsController>(c => c.Create("azure")));
        }
    }
}
