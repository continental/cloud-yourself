using CloudYourself.Backend.Gateways.SelfServicePortalApi.AzureSubscriptions;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.ManagedResourceDeployments
{
    /// <summary>
    /// Link strategy to link a <see cref="DisplayAzureManagedResourceDeploymentVm"/>.
    /// </summary>
    public class DisplayAzureManagedResourceDeploymentVmLinkStrategy : LinkStrategyBase<DisplayAzureManagedResourceDeploymentVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(DisplayAzureManagedResourceDeploymentVm resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<ManagedResourceDeploymentsViewsController>(c => c.GetCreateViewModel(resource.SubscriptionId, resource.ManagedResourceId, "azure")));
            resource.AddLink("subscription", urlHelper.LinkTo<AzureSubscriptionViewsController>(c => c.GetDisplayViewModel(resource.SubscriptionId)));

            // Actions
            resource.AddAction("prepare", "POST", urlHelper.LinkTo<ManagedResourceDeploymentsViewsController>(c => c.Prepare(resource.Id)));

            if (resource["State"].ToString() == "Preparing" && resource["ComplianceState"].ToString() == "Compliant")
            {
                resource.AddAction("commit", "POST", urlHelper.LinkTo<ManagedResourceDeploymentsViewsController>(c => c.Commit(resource.Id)));
            }
        }
    }
}
