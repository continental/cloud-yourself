using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.AutomationApi.CloudAccounts
{
    public class CloudAccountDtoLinkStrategy : LinkStrategyBase<CloudAccountDto>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(CloudAccountDto resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<CloudAccountsController>(c => c.GetById(resource.Id)));
        }
    }
}
