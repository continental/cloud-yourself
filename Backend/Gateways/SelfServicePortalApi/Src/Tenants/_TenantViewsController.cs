using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Fancy.ResourceLinker;
using System;
using Fancy.ResourceLinker.Models;
using CloudYourself.Backend.Gateways.SelfServicePortalApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.Tenants
{
    /// <summary>
    /// A controller to proxy requests to the tenants.
    /// </summary>
    [Authorize]
    [ApiController]
    public class TenantViewsController : ProxyController
    {
        /// <summary>
        /// The application service base urls.
        /// </summary>
        private readonly AppServiceBaseUrlOptions _appServiceBaseUrls;

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantViewsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public TenantViewsController(IOptions<AppServiceBaseUrlOptions> appServiceBaseUrls)
            : base(appServiceBaseUrls.Value.MasterData)
        {
            _appServiceBaseUrls = appServiceBaseUrls.Value;
        }

        /// <summary>
        /// Gets the list tenants view mdoel
        /// </summary>
        /// <returns>The list tenants view model.</returns>
        [HttpGet]
        [Route("/api/views/tenants/list")]
        public async Task<IActionResult> GetListViewModel()
        {
            ListTenantsVm result = new ListTenantsVm();
            result.Tenants = await GetCollectionAsync<DynamicResource>(null, "/api/master-data/tenants");

            foreach (dynamic tenant in result.Tenants)
            {
                tenant.CloudAccountCount = (await GetCollectionAsync<DynamicResource>(null, $"/api/master-data/cloud-accounts?tenantId={tenant.Id}")).Count;
            }

            return Hypermedia(result);
        }

        /// <summary>
        /// Gets the display tenants view mdoel
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The display tenants view model.
        /// </returns>
        [HttpGet]
        [Route("/api/views/tenants/display/{id}")]
        public async Task<IActionResult> GetDisplayViewModel(int id)
        {
            Task<DisplayTenantVm> getTenantVmTask = GetAsync<DisplayTenantVm>(null, $"/api/master-data/tenants/{id}");
            var getRequestedCloudAccountsTask = GetCollectionAsync<DynamicResource>(null, "/api/master-data/cloud-accounts?state=requested");

            DisplayTenantVm tenantVm = await getTenantVmTask;
            tenantVm["RequestedCloudAccounts"] = await getRequestedCloudAccountsTask;
            tenantVm["AzureSummary"] = await GetAsync<DynamicResource>(_appServiceBaseUrls.Azure, $"/api/azure/summary?tenantId={id}");
            tenantVm["BillingSummary"] = await GetAsync<DynamicResource>(_appServiceBaseUrls.Billing, $"/api/billing/summary?tenantId={id}");
            return Hypermedia(tenantVm);
        }

        /// <summary>
        /// Gets the edit tenants view mdoel
        /// </summary>
        /// <returns>The edit tenants view model.</returns>
        [HttpGet]
        [Route("/api/views/tenants/edit/{id}")]
        public async Task<IActionResult> GetEditViewModel(int id)
        {
            Task<EditTenantVm> getTenantVmTask = GetAsync<EditTenantVm>(null, $"/api/master-data/tenants/{id}");
            Task<DynamicResource> getAzureTenantTask = GetAsync<DynamicResource>(_appServiceBaseUrls.Azure, $"/api/azure/tenant-settings/{id}");
            Task<DynamicResource> getAwsTenantTask = GetAsync<DynamicResource>(_appServiceBaseUrls.Aws, $"/api/aws/tenant-settings/{id}");

            EditTenantVm tenantVm = await getTenantVmTask;
            dynamic azureTenantSettings = await getAzureTenantTask;
            dynamic awsTenantSettings = await getAwsTenantTask;
            tenantVm["AzureAppRegistration"] = azureTenantSettings.AppRegistration;
            tenantVm["AzureManagementTarget"] = azureTenantSettings.ManagementTarget;
            tenantVm["AwsIamAccount"] = awsTenantSettings.IamAccount;
            return Hypermedia(tenantVm);
        }

        /// <summary>
        /// Gets a template for a new tenant
        /// </summary>
        /// <returns>A new tenant template.</returns>
        [HttpGet]
        [Route("api/views/tenants/create")]
        public Task<IActionResult> GetCreateViewModel()
        {
            return ProxyGetAsync<CreateTenantVm>(null, "/api/master-data/tenants/template");
        }

        /// <summary>
        /// Creates a new tenant with the specified data.
        /// </summary>
        [HttpPost]
        [Route("api/master-data/tenants")]
        public async Task<IActionResult> Create()
        {
            string postResult = await ProxyPostAsync();
            int newEntityId = Convert.ToInt32(postResult);
            string newResourceUrl = Url.LinkTo<TenantViewsController>(c => c.GetDisplayViewModel(newEntityId));
            return Created(newResourceUrl, new string[] { newResourceUrl });
        }

        /// <summary>
        /// Updates the base data of a tenant.
        /// </summary>
        /// <param name="id">The tenant identifier.</param>
        [HttpPut]
        [Route("/api/master-data/tenants/{id}/base-data")]
        public Task<IActionResult> UpdateBaseData(int id)
        {
            return ProxyPutAsync();
        }

        /// <summary>
        /// Updates the azure app registration settings.
        /// </summary>
        /// <param name="id">The tenant identifier.</param>
        [HttpPut]
        [Route("api/azure/tenant-settings/{id}/app-registration")]
        public Task<IActionResult> UpdateAzureSettingsAppRegistration(int id)
        {
            return ProxyPutAsync(_appServiceBaseUrls.Azure);
        }

        /// <summary>
        /// Updates the azure management target settings.
        /// </summary>
        /// <param name="id">The tenant identifier.</param>
        [HttpPut]
        [Route("api/azure/tenant-settings/{id}/management-target")]
        public Task<IActionResult> UpdateAzureSettingsManagementTarget(int id)
        {
            return ProxyPutAsync(_appServiceBaseUrls.Azure);
        }

        /// <summary>
        /// Updates the azure managed resources settings.
        /// </summary>
        /// <param name="id">The tenant identifier.</param>
        [HttpPut]
        [Route("api/azure/tenant-settings/{id}/managed-resources")]
        public Task<IActionResult> UpdateAzureSettingsManagedResources(int id)
        {
            return ProxyPutAsync(_appServiceBaseUrls.Azure);
        }

        /// <summary>
        /// Updates the aws iam account settings.
        /// </summary>
        /// <param name="id">The tenant identifier.</param>
        [HttpPut]
        [Route("api/aws/tenant-settings/{id}/iam-account")]
        public Task<IActionResult> UpdateAwsSettingsIamAccount(int id)
        {
            return ProxyPutAsync(_appServiceBaseUrls.Aws);
        }
    }
}
