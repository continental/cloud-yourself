using CloudYourself.Backend.Gateways.AutomationApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Collections.Generic;
using Fancy.ResourceLinker.Models;
using Microsoft.AspNetCore.Authorization;

namespace CloudYourself.Backend.Gateways.AutomationApi.Tenants
{
    /// <summary>
    /// A controller to proxy requests to the tenants.
    /// </summary>
    [Authorize]
    [ApiController]
    public class TenantsController : ProxyController
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<TenantsController> _logger;

        /// <summary>
        /// The application service base urls.
        /// </summary>
        private readonly AppServiceBaseUrlOptions _appServiceBaseUrls;

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public TenantsController(ILogger<TenantsController> logger, IOptions<AppServiceBaseUrlOptions> appServiceBaseUrls)
            : base(appServiceBaseUrls.Value.MasterData)
        {
            _logger = logger;
            _appServiceBaseUrls = appServiceBaseUrls.Value;
        }

        /// <summary>
        /// Gets all tenants.
        /// </summary>
        /// <returns>A list of tenants.</returns>
        [HttpGet]
        [Route("/api/master-data/tenants")]
        public async Task<IActionResult> GetAll()
        {
            List<TenantDto> result = await GetCollectionAsync<TenantDto>();

            foreach(TenantDto tenant in result)
            {
                await AddCloudProviderSettings(tenant);
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
            TenantDto result = await GetAsync<TenantDto>(null, $"/api/master-data/tenants/{id}");
            await AddCloudProviderSettings(result);
            return Hypermedia(result);
        }

        /// <summary>
        /// Adds the cloud provider settings.
        /// </summary>
        /// <param name="tenant">The tenant to add the cloud provider settings to.</param>
        private async Task AddCloudProviderSettings(TenantDto tenant)
        {
            tenant["AzureSettings"] = await GetAsync<DynamicResource>(_appServiceBaseUrls.Azure, $"/api/azure/tenant-settings/{tenant.Id}");
            tenant["AwsSettings"] = await GetAsync<DynamicResource>(_appServiceBaseUrls.Aws, $"/api/aws/tenant-settings/{tenant.Id}");
        }
    }
}
