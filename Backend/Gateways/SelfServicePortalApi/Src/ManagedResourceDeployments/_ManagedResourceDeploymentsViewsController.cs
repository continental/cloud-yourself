using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Fancy.ResourceLinker;
using System;
using Fancy.ResourceLinker.Models;
using CloudYourself.Backend.Gateways.SelfServicePortalApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.ManagedResourceDeployments
{
    /// <summary>
    /// A controller to proxy requests to the managed resources.
    /// </summary>
    [Authorize]
    [ApiController]
    public class ManagedResourceDeploymentsViewsController : ProxyController
    {
        /// <summary>
        /// The application service base urls.
        /// </summary>
        private readonly AppServiceBaseUrlOptions _appServiceBaseUrls;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedResourceDeploymentsViewsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ManagedResourceDeploymentsViewsController(IOptions<AppServiceBaseUrlOptions> appServiceBaseUrls)
            : base(appServiceBaseUrls.Value.Azure)
        {
            _appServiceBaseUrls = appServiceBaseUrls.Value;
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
        [Route("/api/views/managed-resource-deployments/display/{id}")]
        public async Task<IActionResult> GetDisplayViewModel(int id, string type)
        {
            ResourceBase result;

            switch (type)
            {
                case "azure":
                    DisplayAzureManagedResourceDeploymentVm viewModel = await GetAsync<DisplayAzureManagedResourceDeploymentVm>(null, $"/api/azure/managed-resource-deployments/{id}");
                    dynamic subscription = await GetAsync<DynamicResource>(null, $"/api/azure/subscriptions/{viewModel.SubscriptionId}");
                    viewModel.ManagedResource = await GetAsync<DynamicResource>(null, $"/api/azure/managed-resources/{viewModel.ManagedResourceId}");
                    viewModel.ManagedResource["Type"] = "azure";
                    viewModel["ComplianceState"] = subscription.Compliance.State;
                    result = viewModel;
                    break;
                default:
                    return BadRequest();
            }

            return Hypermedia(result);
        }

        /// <summary>
        /// Gets a template for a new managed resource
        /// </summary>
        /// <param name="targetSubscriptionId">The target subscription identifier.</param>
        /// <param name="managedResourceId">The managed resource identifier.</param>
        /// <param name="type">The resource type.</param>
        /// <returns>
        /// A new managed resource template.
        /// </returns>
        [HttpGet]
        [Route("api/views/managed-resource-deployments/create")]
        public async Task<IActionResult> GetCreateViewModel(int targetSubscriptionId, int managedResourceId, string type)
        {
            ResourceBase result;
            ResourceBase managedResource;

            switch (type)
            {
                case "azure":
                    result = await GetAsync<CreateAzureManagedResourceDeploymentVm>(null, $"/api/azure/managed-resource-deployments/template?managedResourceId={managedResourceId}&targetSubscriptionId={targetSubscriptionId}");
                    managedResource = await GetAsync<DynamicResource>(null, $"/api/azure/managed-resources/{managedResourceId}");
                    break;
                default:
                    return BadRequest();
            }

            // Assign base data of managed resource to resulting view model
            result["BaseData"] = managedResource["BaseData"];

            return Hypermedia(result);
        }

        /// <summary>
        /// Creates a new managed resource with the specified data.
        /// </summary>
        /// <param name="type">The resource type.</param>
        [HttpPost]
        [Route("api/azure/managed-resource-deployments")]
        public async Task<IActionResult> Create(string type)
        {
            string postResult = await ProxyPostAsync();
            int newEntityId = Convert.ToInt32(postResult);
            string newResourceUrl = Url.LinkTo<ManagedResourceDeploymentsViewsController>(c => c.GetDisplayViewModel(newEntityId, type));
            return Created(newResourceUrl, new string[] { newResourceUrl });
        }

        /// <summary>
        /// Starts preparation phase of the specified managed resource deployment.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpPost]
        [Route("api/azure/managed-resource-deployments/{id}/prepare")]
        public async Task<IActionResult> Prepare(int id)
        {
            try
            {
                await ProxyPostAsync();
            }
            catch (ProxyResponseException e)
            {
                return BadRequest(new { ErrorCode = e.ErrorCode, ErrorMessage = e.Message });
            }

            return NoContent();
        }

        /// <summary>
        /// Commits the specified managed resource deployment.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpPost]
        [Route("api/azure/managed-resource-deployments/{id}/commit")]
        public async Task<IActionResult> Commit(int id)
        {
            try
            {
                await ProxyPostAsync();
            }
            catch(ProxyResponseException e)
            {
                return BadRequest(new { ErrorCode = e.ErrorCode, ErrorMessage = e.Message });
            }
            
            return NoContent();
        }
    }
}
