using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.ManagedResources
{
    /// <summary>
    /// Link strategy to link a <see cref="EditManagedResourceVm"/>.
    /// </summary>
    public class EditManagedResourceVmLinkStrategy : LinkStrategyBase<EditManagedResourceVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(EditManagedResourceVm resource, IUrlHelper urlHelper)
        {
            dynamic vm = resource;

            // Links
            resource.AddLink("self", urlHelper.LinkTo<ManagedResourceViewsController>(c => c.GetEditViewModel(resource.Id, resource.Type)));

            // Actions
            vm.BaseData.AddAction("update", "PUT", urlHelper.LinkTo<ManagedResourceViewsController>(c => c.UpdateAzureBaseData(resource.Id)));
            vm.ComplianceSettings.AddAction("update", "PUT", urlHelper.LinkTo<ManagedResourceViewsController>(c => c.UpdateAzureComplianceSettings(resource.Id)));

            if (resource.ContainsKey("ArmTemplate"))
            {
                vm.ArmTemplate.AddAction("update", "PUT", urlHelper.LinkTo<ManagedResourceViewsController>(c => c.UpdateAzureArmTemplate(resource.Id)));
            }
        }
    }
}
