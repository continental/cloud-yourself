using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.AwsAccounts
{
    public class DisplayAwsAccountVmLinkStrategy : LinkStrategyBase<DisplayAwsAccountVm>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(DisplayAwsAccountVm resource, IUrlHelper urlHelper)
        {
            resource.AddLink("self", urlHelper.LinkTo<AwsAccountViewsController>(c => c.GetDisplayViewModel(resource.Id)));
        }
    }
}
