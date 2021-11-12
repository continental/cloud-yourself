using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.AwsAccounts
{
    public class AddAccountVmLinkStrategy : LinkStrategyBase<AddAccountVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(AddAccountVm resource, IUrlHelper urlHelper)
        {
            dynamic vm = resource;

            vm.AddLink("self", urlHelper.LinkTo<AwsAccountViewsController>(c => c.GetAddViewModel(resource.TenantId, resource.CloudAccountId)));

            foreach (dynamic unmanagedAccount in vm.UnmanagedAccounts)
            {
                unmanagedAccount.AddAction("add", "POST", urlHelper.LinkTo<AwsAccountViewsController>(c => c.Create()));
            }
        }
    }
}
