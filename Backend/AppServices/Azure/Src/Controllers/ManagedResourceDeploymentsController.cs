using CloudYourself.Backend.AppServices.Azure.Aggregates.ManagedResource;
using CloudYourself.Backend.AppServices.Azure.Aggregates.ManagedResourceDeployment;
using CloudYourself.Backend.AppServices.Azure.Aggregates.Subscription;
using CloudYourself.Backend.AppServices.Azure.Aggregates.Tennant;
using CloudYourself.Backend.AppServices.Azure.Dtos;
using CloudYourself.Backend.AppServices.Azure.Infrastructure;
using CloudYourself.Backend.AppServices.Azure.Services;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.Azure.Controllers
{
    /// <summary>
    /// A controller to manage azure managed resources.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiController]
    public class ManagedResourceDeployments : ControllerBase
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly AzureDbContext _dbContext;

        /// <summary>
        /// The deployment service.
        /// </summary>
        private readonly DeploymentService _deploymentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedResourceDeployments" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="deploymentService">The deployment service.</param>
        public ManagedResourceDeployments(AzureDbContext dbContext, DeploymentService deploymentService)
        {
            _dbContext = dbContext;
            _deploymentService = deploymentService;
        }

        /// <summary>
        /// Gets a managed resource deployment by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The managed resource deployment.</returns>
        [HttpGet]
        [Route("api/azure/managed-resource-deployments/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ManagedResourceDeployment result = await _dbContext.ManagedResourceDeployments.SingleAsync(mr => mr.Id == id);
            return Ok(result);
        }

        /// <summary>
        /// Gets managed-resource deployments filtered.
        /// </summary>
        /// <param name="tenantId">The subscription identifier.</param>
        /// <returns>
        /// A list of managed resource deployments.
        /// </returns>
        [HttpGet]
        [Route("api/azure/managed-resource-deployments")]
        public async Task<IActionResult> GetFiltered(int subscriptionId)
        {
            IQueryable<ManagedResourceDeployment> query = _dbContext.ManagedResourceDeployments;

            query = query.Where(mrd => mrd.SubscriptionId == subscriptionId);

            List<ManagedResourceDeployment> result = await query.ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Prepares the subscription for the specified managed resource deployment.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpPost]
        [Route("api/azure/managed-resource-deployments/{id}/prepare")]
        public async Task<IActionResult> Prepare(int id)
        {
            ManagedResourceDeployment deployment = await _dbContext.ManagedResourceDeployments.SingleAsync(mr => mr.Id == id);

            // Get Subscription and tenant settings
            ManagedResource managedResource = await _dbContext.ManagedResources.SingleAsync(mr => mr.Id == deployment.ManagedResourceId);
            Subscription subscription = await _dbContext.Subscriptions.SingleAsync(s => s.Id == deployment.SubscriptionId);
            TenantSettings tenantSettings = await _dbContext.TenantSettings.SingleAsync(ts => ts.Id == subscription.TenantId);

            try
            {
                await _deploymentService.CreateInitiativeAssignment(tenantSettings, 
                                                                    subscription.SubscriptionId, 
                                                                    managedResource.ComplianceSettings.InitiativeDefinitionId, 
                                                                    managedResource.ComplianceSettings.InitiativeAssignmentName);
            }
            catch (ErrorObjectException e)
            {
                return BadRequest(e.ToErrorObject());
            }

            // Save changed state to deployment
            deployment.PrepareDate = DateTime.Now;
            deployment.State = ManagedResourceDeploymentState.Preparing;

            await _dbContext.SaveChangesAsync();

            // Trigger a new policy evaulation for the affected subscription
            subscription.Compliance.State = ComplianceState.Evaluating;
            subscription.Compliance.PolicyEvaluationResultUrl = await _deploymentService.TriggerPolicyEvaluation(tenantSettings, subscription.SubscriptionId);

            await _dbContext.SaveChangesAsync();

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
            ManagedResourceDeployment deployment = await _dbContext.ManagedResourceDeployments.SingleAsync(mr => mr.Id == id);

            // Get Subscription and tenant settings
            ManagedResource managedResource = await _dbContext.ManagedResources.SingleAsync(mr => mr.Id == deployment.ManagedResourceId);
            Subscription subscription = await _dbContext.Subscriptions.SingleAsync(s => s.Id == deployment.SubscriptionId);
            TenantSettings tenantSettings = await _dbContext.TenantSettings.SingleAsync(ts => ts.Id == subscription.TenantId);

            try
            {
                await _deploymentService.DeployManagedResourceAsync(tenantSettings, subscription.SubscriptionId, deployment.Name, managedResource.ArmTemplate.Template, deployment.Parameters);
            }
            catch(ErrorObjectException e)
            {
                return BadRequest(e.ToErrorObject());
            }

            deployment.CommitDate = DateTime.Now;
            deployment.State = ManagedResourceDeploymentState.Commited;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Gets the template for a new managed resource deployment.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <returns>The subscription template.</returns>
        [HttpGet]
        [Route("api/azure/managed-resource-deployments/template")]
        public async Task<IActionResult> GetNewTemplate(int managedResourceId, int targetSubscriptionId)
        {
            var result = new NewManagedResourceDeploymentDto { ManagedResourceId = managedResourceId, TargetSubscriptionId = targetSubscriptionId };

            ManagedResource managedResource = await _dbContext.ManagedResources.SingleAsync(mr => mr.Id == managedResourceId);
            result.TenantId = managedResource.TenantId;
            result.DeployParams = _deploymentService.GetParameterMetadata(managedResource.ArmTemplate.Template);

            return Ok(result);
        }

        /// <summary>
        /// Creates a new subscription.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <param name="existingSubscriptionDto">The existing subscription dto.</param>
        /// <returns>
        /// The link to the new subscription.
        /// </returns>
        [HttpPost]
        [Route("api/azure/managed-resource-deployments")]
        public async Task<IActionResult> Create([FromBody] NewManagedResourceDeploymentDto managedResourceDeploymentDto)
        {
            ManagedResourceDeployment newManagedResourceDeployment = new ManagedResourceDeployment();
            _dbContext.ManagedResourceDeployments.Add(newManagedResourceDeployment);

            Subscription subscription = await _dbContext.Subscriptions.SingleAsync(s => s.Id == managedResourceDeploymentDto.TargetSubscriptionId);
            TenantSettings tenantSettings = await _dbContext.TenantSettings.SingleAsync(ts => ts.Id == subscription.TenantId);

            try
            {
                // Create resource group
                await _deploymentService.CreateResourceGroupAsync(tenantSettings, subscription.SubscriptionId, managedResourceDeploymentDto.Name);
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            // Set base data
            newManagedResourceDeployment.ManagedResourceId = managedResourceDeploymentDto.ManagedResourceId;
            newManagedResourceDeployment.SubscriptionId = managedResourceDeploymentDto.TargetSubscriptionId;
            newManagedResourceDeployment.Parameters = _deploymentService.BuildParametersJson(managedResourceDeploymentDto.DeployParams);
            newManagedResourceDeployment.Name = managedResourceDeploymentDto.Name;

            await _dbContext.SaveChangesAsync();

            return Created(Url.LinkTo<ManagedResourcesController>(c => c.GetById(newManagedResourceDeployment.Id)), newManagedResourceDeployment.Id);
        }

        /// <summary>
        /// Deletes a managed resource deployments by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete]
        [Route("api/azure/managed-resource-deployments/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            ManagedResourceDeployment managedResourceDeployment = await _dbContext.ManagedResourceDeployments.SingleAsync(mr => mr.Id == id);

            // Get Subscription and tenant settings
            Subscription subscription = await _dbContext.Subscriptions.SingleAsync(s => s.Id == managedResourceDeployment.SubscriptionId);
            TenantSettings tenantSettings = await _dbContext.TenantSettings.SingleAsync(ts => ts.Id == subscription.TenantId);

            // ToDo: Delete deployed ARM Template resources

            _dbContext.ManagedResourceDeployments.Remove(managedResourceDeployment);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
