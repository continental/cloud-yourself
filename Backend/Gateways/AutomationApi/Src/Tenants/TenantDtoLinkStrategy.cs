using CloudYourself.Backend.Gateways.AutomationApi.AzureSubscriptions;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.AutomationApi.Tenants
{
    /// <summary>
    /// Link strategy to link a <see cref="TenantDto"/>.
    /// </summary>
    public class TenantDtoLinkStrategy : LinkStrategyBase<TenantDto>
    {
        /// <summary>
        /// Links the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="urlHelper">The URL helper.</param>
        protected override void LinkResourceInternal(TenantDto resource, IUrlHelper urlHelper)
        {
            // Links
            resource.AddLink("self", urlHelper.LinkTo<TenantsController>(c => c.GetById(resource.Id)));
            resource.AddLink("azureSubscriptions", urlHelper.LinkTo<AzureSubscriptionsController>(c => c.GetFiltered(resource.Id, null)));
        }
    }
}
