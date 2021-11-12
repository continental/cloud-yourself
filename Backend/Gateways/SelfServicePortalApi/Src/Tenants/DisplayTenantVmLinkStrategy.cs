using CloudYourself.Backend.Gateways.SelfServicePortalApi.CloudAccounts;
using CloudYourself.Backend.Gateways.SelfServicePortalApi.Costs;
using CloudYourself.Backend.Gateways.SelfServicePortalApi.ManagedResources;
using CloudYourself.Backend.Gateways.SelfServicePortalApi.PayerAccounts;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.Tenants
{
    /// <summary>
    /// Link strategy to link a <see cref="DisplayTenantVm"/>.
    /// </summary>
    public class DisplayManagedResourceVmLinkStrategy : LinkStrategyBase<DisplayTenantVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(DisplayTenantVm resource, IUrlHelper urlHelper)
        {
            dynamic vm = resource;

            // Links
            resource.AddLink("self", urlHelper.LinkTo<TenantViewsController>(c => c.GetDisplayViewModel(resource.Id)));
            resource.AddLink("editTenantVm", urlHelper.LinkTo<TenantViewsController>(c => c.GetEditViewModel(resource.Id)));
            resource.AddLink("listManagedResourcesVm", urlHelper.LinkTo<ManagedResourceViewsController>(c => c.GetListViewModel(resource.Id, null, null)));
            resource.AddLink("listPayerAccountsVm", urlHelper.LinkTo<PayerAccountViewsController>(c => c.GetListViewModel(resource.Id)));
            resource.AddLink("costMatrixVm", urlHelper.LinkTo<CostViewsController>(c => c.GetCostMatrixViewModel(resource.Id)));

            foreach(dynamic cloudAccount in vm.RequestedCloudAccounts)
            {
                cloudAccount.AddAction("approve", "POST", urlHelper.LinkTo<CloudAccountViewsController>(c => c.SetState("Approved")));
            }
        }
    }
}
