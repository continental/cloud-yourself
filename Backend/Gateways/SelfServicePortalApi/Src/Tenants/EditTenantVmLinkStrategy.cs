using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.Tenants
{
    /// <summary>
    /// Link strategy to link a <see cref="EditTenantVm"/>.
    /// </summary>
    public class EditTenantVmLinkStrategy : LinkStrategyBase<EditTenantVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(EditTenantVm resource, IUrlHelper urlHelper)
        {
            dynamic vm = resource;

            // Links
            resource.AddLink("self", urlHelper.LinkTo<TenantViewsController>(c => c.GetEditViewModel(resource.Id)));

            // Actions
            vm.BaseData.AddAction("update", "PUT", urlHelper.LinkTo<TenantViewsController>(c => c.UpdateBaseData(resource.Id)));

            if (resource.ContainsKey("AzureAppRegistration"))
            {
                vm.AzureAppRegistration.AddAction("update", "PUT", urlHelper.LinkTo<TenantViewsController>(c => c.UpdateAzureSettingsAppRegistration(resource.Id)));
            }

            if (resource.ContainsKey("AzureManagementTarget"))
            {
                vm.AzureManagementTarget.AddAction("update", "PUT", urlHelper.LinkTo<TenantViewsController>(c => c.UpdateAzureSettingsManagementTarget(resource.Id)));
            }

            if (resource.ContainsKey("AwsIamAccount"))
            {
                vm.AwsIamAccount.AddAction("update", "PUT", urlHelper.LinkTo<TenantViewsController>(c => c.UpdateAwsSettingsIamAccount(resource.Id)));
            }
        }
    }
}
