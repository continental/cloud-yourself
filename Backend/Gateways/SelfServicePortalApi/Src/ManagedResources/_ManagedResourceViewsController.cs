using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Fancy.ResourceLinker;
using System;
using Fancy.ResourceLinker.Models;
using CloudYourself.Backend.Gateways.SelfServicePortalApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.ManagedResources
{
    /// <summary>
    /// A controller to proxy requests to the managed resources.
    /// </summary>
    [Authorize]
    [ApiController]
    public class ManagedResourceViewsController : ProxyController
    {
        /// <summary>
        /// The application service base urls.
        /// </summary>
        private readonly AppServiceBaseUrlOptions _appServiceBaseUrls;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedResourceViewsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ManagedResourceViewsController(IOptions<AppServiceBaseUrlOptions> appServiceBaseUrls)
            : base(appServiceBaseUrls.Value.Azure)
        {
            _appServiceBaseUrls = appServiceBaseUrls.Value;
        }

        /// <summary>
        /// Gets the list managed resources view model
        /// </summary>
        /// <param name="tenantId">The tenant identifier to show the managed resources for.</param>
        /// <param name="type">The type of managed resources to show.</param>
        /// <param name="deploymentSubscriptionId">The deployment subscription identifier of a target azure subscription for deployment.</param>
        /// <returns>
        /// The list managed resources view model.
        /// </returns>
        [HttpGet]
        [Route("/api/views/managed-resources/list")]
        public async Task<IActionResult> GetListViewModel(int tenantId, string type, int? deploymentSubscriptionId)
        {
            ListManagedResourcesVm result = new ListManagedResourcesVm { TenantId = tenantId };
            result.ManagedResources = await GetCollectionAsync<DynamicResource>(null, $"/api/azure/managed-resources?tenantId={tenantId}");

            foreach(DynamicResource managedResource in result.ManagedResources)
            {
                managedResource["Type"] = "azure";
            }

            result.DeploymentSubscriptionId = deploymentSubscriptionId;

            return Hypermedia(result);
        }

        /// <summary>
        /// Gets the display managed resource view mdoel
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The display managed resource view model.
        /// </returns>
        [HttpGet]
        [Route("/api/views/managed-resources/display/{id}")]
        public async Task<IActionResult> GetDisplayViewModel(int id, string type)
        {
            DisplayManagedResourceVm result;

            switch (type)
            {
                case "azure":
                    result = await GetAsync<DisplayManagedResourceVm>(null, $"/api/azure/managed-resources/{id}");
                    break;
                default:
                    return BadRequest();
            }

            result.Type = type;

            return Hypermedia(result);
        }

        /// <summary>
        /// Gets the edit managed resource view mdoel
        /// </summary>
        /// <returns>The edit managed resource view model.</returns>
        [HttpGet]
        [Route("/api/views/managed-resources/edit/{id}")]
        public async Task<IActionResult> GetEditViewModel(int id, string type)
        {
            EditManagedResourceVm result;

            switch (type)
            {
                case "azure":
                    result = await GetAsync<EditManagedResourceVm>(null, $"/api/azure/managed-resources/{id}");
                    break;
                default:
                    return BadRequest();
            }

            result.Type = type;

            return Hypermedia(result);
        }

        /// <summary>
        /// Gets a template for a new managed resource
        /// </summary>
        /// <returns>A new managed resource template.</returns>
        [HttpGet]
        [Route("api/views/managed-resources/create")]
        public async Task<IActionResult> GetCreateViewModel(int tenantId, string type)
        {
            CreateManagedResourceVm result;

            switch (type)
            {
                case "azure":
                    result = await GetAsync<CreateManagedResourceVm>(null, $"/api/azure/managed-resources/template?tenantId={tenantId}");
                    break;
                default:
                    return BadRequest();
            }

            result.TenantId = tenantId;
            result.Type = type;

            return Hypermedia(result);
        }

        /// <summary>
        /// Creates a new managed resource with the specified data.
        /// </summary>
        [HttpPost]
        [Route("api/azure/managed-resources")]
        public async Task<IActionResult> Create(string type)
        {
            string postResult = await ProxyPostAsync();
            int newEntityId = Convert.ToInt32(postResult);
            string newResourceUrl = Url.LinkTo<ManagedResourceViewsController>(c => c.GetDisplayViewModel(newEntityId, type));
            return Created(newResourceUrl, new string[] { newResourceUrl });
        }

        /// <summary>
        /// Deletes an azure managed resource by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete]
        [Route("api/azure/managed-resources/{id}")]
        public Task<IActionResult> DeleteAzureManagedResourceById(int id)
        {
            return ProxyDeleteAsync();
        }

        /// <summary>
        /// Updates the base data of an azure managed resource.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="baseData">The base data.</param>
        [HttpPut]
        [Route("api/azure/managed-resources/{id}/base-data")]
        public Task<IActionResult> UpdateAzureBaseData(int id)
        {
            return ProxyPutAsync();
        }

        /// <summary>
        /// Updates the azure compliance settings.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpPut]
        [Route("api/azure/managed-resources/{id}/compliance-settings")]
        public Task<IActionResult> UpdateAzureComplianceSettings(int id)
        {
            return ProxyPutAsync();
        }

        /// <summary>
        /// Updates the arm template of an azure managed resource.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="baseData">The base data.</param>
        [HttpPut]
        [Route("api/azure/managed-resources/{id}/arm-template")]
        public Task<IActionResult> UpdateAzureArmTemplate(int id)
        {
            return ProxyPutAsync();
        }
    }
}
