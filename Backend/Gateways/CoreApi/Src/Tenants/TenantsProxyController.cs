using CloudYourself.Backend.Gateways.CoreApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Fancy.ResourceLinker;
using System;
using Fancy.ResourceLinker.Models;
using CloudYourself.Backend.Gateways.CoreApi.CloudAccounts;

namespace CloudYourself.Backend.Gateways.CoreApi.Tenants
{
    /// <summary>
    /// A controller to proxy requests to the tenants.
    /// </summary>>
    [ApiController]
    public class TenantsProxyController : ProxyController
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<TenantsProxyController> _logger;

        /// <summary>
        /// The application service base urls.
        /// </summary>
        private readonly AppServiceBaseUrlOptions _appServiceBaseUrls;

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantsProxyController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public TenantsProxyController(ILogger<TenantsProxyController> logger, IOptions<AppServiceBaseUrlOptions> appServiceBaseUrls)
            : base(appServiceBaseUrls.Value.CloudAccounts)
        {
            _logger = logger;
            _appServiceBaseUrls = appServiceBaseUrls.Value;
        }

        /// <summary>
        /// Gets all tenants.
        /// </summary>
        /// <returns>A list of tenants.</returns>
        [HttpGet]
        [Route("/api/tenants")]
        public async Task<IActionResult> GetAll()
        {
            TenantsVm result = new TenantsVm();
            result.Tenants = await GetCollectionAsync<TenantVm>();
            
            foreach(TenantVm tenant in result.Tenants)
            {
                tenant["CloudAccountCount"] = (await GetCollectionAsync<CloudAccountVm>(null, $"/api/cloud-accounts?tenantId={tenant.Id}")).Count;
            }

            return Hypermedia(result);
        }

        /// <summary>
        /// Gets a specific tenant identified by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A tenant.</returns>
        [HttpGet]
        [Route("/api/tenants/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            TenantVm tenantVm = await GetAsync<TenantVm>();
            dynamic azureSubscriptionsTenant = await GetAsync<DynamicResource>(_appServiceBaseUrls.AzureSubscriptions, $"/api/tenants/{id}/azure-subscriptions");
            tenantVm["AzureAppRegistration"] = azureSubscriptionsTenant.AppRegistration;
            tenantVm["AzureManagementTarget"] = azureSubscriptionsTenant.ManagementTarget;
            tenantVm["RequestedCloudAccounts"] = await GetCollectionAsync<CloudAccountVm>(null, "/api/cloud-accounts?state=requested");
            return Hypermedia(tenantVm);
        }

        /// <summary>
        /// Gets a template for a new tenant
        /// </summary>
        /// <returns>A new tenant template.</returns>
        [HttpGet]
        [Route("api/tenants/template")]
        public Task<IActionResult> GetTemplate()
        {
            return ProxyGetAsync<NewTenantVm>();
        }

        /// <summary>
        /// Creates a new tenant with the specified data.
        /// </summary>
        /// <param name="newTenantDto">The new tenant dto.</param>
        [HttpPost]
        [Route("api/tenants")]
        public async Task<IActionResult> Create()
        {
            string postResult = await ProxyPostAsync();
            int newEntityId = Convert.ToInt32(postResult);
            string newResourceUrl = Url.LinkTo<TenantsProxyController>(c => c.GetById(newEntityId));
            return Created(newResourceUrl, new string[] { newResourceUrl });
        }

        /// <summary>
        /// Updates the base data of a tenant.
        /// </summary>
        /// <param name="id">The tenant identifier.</param>
        [HttpPut]
        [Route("/api/tenants/{id}/base-data")]
        public Task<IActionResult> UpdateBaseData(int id)
        {
            return ProxyPutAsync();
        }

        /// <summary>
        /// Updates the azure subscirptions app registration settings.
        /// </summary>
        /// <param name="id">The tenant identifier.</param>
        [HttpPut]
        [Route("api/tenants/{id}/azure-subscriptions/app-registration")]
        public Task<IActionResult> UpdateAzureSubsciptionsAppRegistration(int id)
        {
            return ProxyPutAsync(_appServiceBaseUrls.AzureSubscriptions);
        }

        /// <summary>
        /// Updates the azure subscriptions management target settings.
        /// </summary>
        /// <param name="id">The tenant identifier.</param>
        [HttpPut]
        [Route("api/tenants/{id}/azure-subscriptions/management-target")]
        public Task<IActionResult> UpdateAzureSubsciptionsManagementTarget(int id)
        {
            return ProxyPutAsync(_appServiceBaseUrls.AzureSubscriptions);
        }
    }
}
