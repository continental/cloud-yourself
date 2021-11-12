using CloudYourself.Backend.AppServices.Azure.Aggregates.Subscription;
using CloudYourself.Backend.AppServices.Azure.Aggregates.Tennant;
using CloudYourself.Backend.AppServices.Azure.Dtos;
using CloudYourself.Backend.AppServices.Azure.Infrastructure;
using CloudYourself.Backend.AppServices.Azure.Services;
using Fancy.ResourceLinker;
using Fancy.ResourceLinker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.Azure.Controllers
{
    /// <summary>
    /// A controller to manage azure subscriptions.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly AzureDbContext _dbContext;

        /// <summary>
        /// The database context options.
        /// </summary>
        private readonly DbContextOptions<AzureDbContext> _dbContextOptions;

        /// <summary>
        /// The azure subscription service.
        /// </summary>
        private readonly SubscriptionService _azureSubscriptionService;

        /// <summary>
        /// The deployment service.
        /// </summary>
        private readonly DeploymentService _deploymentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionsController" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="dbContextOptions">The database context options.</param>
        /// <param name="azureSubscriptionService">The azure subscription service.</param>
        /// <param name="deploymentService">The deployment service.</param>
        public SubscriptionsController(AzureDbContext dbContext, 
                                       DbContextOptions<AzureDbContext> dbContextOptions, 
                                       SubscriptionService azureSubscriptionService,
                                       DeploymentService deploymentService)
        {
            _dbContext = dbContext;
            _dbContextOptions = dbContextOptions;
            _azureSubscriptionService = azureSubscriptionService;
            _deploymentService = deploymentService;
        }

        /// <summary>
        /// Gets all unmanaged subscriptions of a tenant.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        [HttpGet]
        [Route("api/azure/subscriptions/unmanaged")]
        public async Task<IActionResult> GetAllUnmanaged(int tenantId)
        {
            TenantSettings tenantSettings = await _dbContext.TenantSettings.SingleAsync(ts => ts.Id == tenantId);
            List<UnmanagedSubscriptionDto> result = await _azureSubscriptionService.GetAllUnmanagedByTenantIdAsync(tenantSettings);
            return Ok(result);
        }

        /// <summary>
        /// Gets a subscription by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpGet]
        [Route("api/azure/subscriptions/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Subscription result = await _dbContext.Subscriptions.SingleAsync(azs => azs.Id == id);

            // Check if the subscription has a running policy evaluation
            if(!string.IsNullOrEmpty(result.Compliance.PolicyEvaluationResultUrl) || result.Compliance.State != ComplianceState.Compliant)
            {
                TenantSettings tenantSettings = await _dbContext.TenantSettings.SingleAsync(ts => ts.Id == result.TenantId);
                result.Compliance.State = await _deploymentService.ReadPolicyEvaluationResult(tenantSettings, result.Compliance.PolicyEvaluationResultUrl, result.SubscriptionId);
                
                if(result.Compliance.State != ComplianceState.Evaluating)
                {
                    // Delete the evaluation url if no evaluation is running anymore
                    result.Compliance.PolicyEvaluationResultUrl = null;
                }

                await _dbContext.SaveChangesAsync();
            }

            return Ok(result);
        }

        /// <summary>
        /// Gets subscirptions filtered.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <returns>
        /// A list of subscriptions.
        /// </returns>
        [HttpGet]
        [Route("api/azure/subscriptions")]
        public async Task<IActionResult> GetFiltered(int? tenantId = null, int? cloudAccountId = null)
        {
            IQueryable<Subscription> query = _dbContext.Subscriptions;

            if(tenantId.HasValue)
            {
                query = query.Where(azs => azs.TenantId == tenantId.Value);
            }

            if (cloudAccountId.HasValue)
            {
                query = query.Where(azs => azs.CloudAccountId == cloudAccountId.Value);
            }

            List<Subscription> result = await query.ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets the template for a new subscription.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <returns>The subscription template</returns>
        [HttpGet]
        [Route("api/azure/subscriptions/template")]
        public IActionResult GetNewTemplate(int tenantId, int cloudAccountId)
        {
            return Ok(new NewSubscriptionDto { TenantId = tenantId, CloudAccountId = cloudAccountId });
        }

        /// <summary>
        /// Creates a new subscription.
        /// </summary>
        /// <param name="subscriptionDto">The subscription dto.</param>
        /// <returns>
        /// The link to the new subscription.
        /// </returns>
        [HttpPost]
        [Route("api/azure/subscriptions")]
        public async Task<IActionResult> Create([FromBody] DynamicResource subscriptionDto)
        {
            Subscription newSubscription = new Subscription();
            _dbContext.Subscriptions.Add(newSubscription);

            newSubscription.TenantId = Convert.ToInt32(subscriptionDto["TenantId"]);
            newSubscription.CloudAccountId = Convert.ToInt32(subscriptionDto["CloudAccountId"]);

            TenantSettings tenantSettings = await _dbContext.TenantSettings.SingleAsync(ts => ts.Id == newSubscription.TenantId);

            if (subscriptionDto.ContainsKey("SubscriptionId"))
            {
                // Add an existing subscription
                newSubscription.Name = subscriptionDto["Name"].ToString();
                newSubscription.SubscriptionId = subscriptionDto["SubscriptionId"].ToString();
                newSubscription.State = (SubscriptionState)Enum.Parse(typeof(SubscriptionState), subscriptionDto["State"].ToString());
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                // Create a new subscription
                newSubscription.Name = subscriptionDto["Name"].ToString();
                newSubscription.CreationOperationUrl = await _azureSubscriptionService.StartCreateSubscriptionOperationAsync(tenantSettings, newSubscription.Name);
                await _dbContext.SaveChangesAsync();
                WatchSubscriptionCreationAsync(newSubscription.Id);
            }

            return Created(Url.LinkTo<SubscriptionsController>(c => c.GetById(newSubscription.Id)), newSubscription.Id);
        }

        /// <summary>
        /// Updates the compliance data of a subscription.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="complianceState">Data of the compliance.</param>
        [HttpPut]
        [Route("api/azure/subscriptions/{id}/compliance")]
        public async Task<IActionResult> UpdateCompliance(int id, [FromBody] Compliance compliance)
        {
            Subscription subscription = await _dbContext.Subscriptions.SingleAsync(azs => azs.Id == id);
            subscription.Compliance = compliance;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Cancels a subscription by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/azure/subscriptions/{id}")]
        public async Task<IActionResult> CancelById(int id)
        {
            Subscription subscription = await _dbContext.Subscriptions.SingleAsync(azs => azs.Id == id);
            TenantSettings tenantSettings = await _dbContext.TenantSettings.SingleAsync(ts => ts.Id == subscription.TenantId);

            if (string.IsNullOrEmpty(subscription.SubscriptionLink))
            {
                subscription.SubscriptionLink = await _azureSubscriptionService.GetSubscriptionLinkAsync(tenantSettings, subscription.CreationOperationUrl);
                await _dbContext.SaveChangesAsync();
            }

            await _azureSubscriptionService.CancelSubscriptionAsync(tenantSettings, subscription.SubscriptionLink);

            subscription.State = SubscriptionState.Cancelled;
            subscription.Compliance.Reset();
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Watches the subscription creation and updates final id of subscription.
        /// </summary>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <returns>
        /// A task indicating the completion of the asynchronous operation.
        /// </returns>
        private async void WatchSubscriptionCreationAsync(int subscriptionId)
        {
            // Loop for three tries to get subscription link
            for (int i = 0; i < 3; i++)
            {
                await Task.Delay(10000);

                using (AzureDbContext dbContext = new AzureDbContext(_dbContextOptions))
                {
                    Subscription subscription = await dbContext.Subscriptions.SingleAsync(s => s.Id == subscriptionId);
                    TenantSettings tenantSettings = await dbContext.TenantSettings.SingleAsync(ts => ts.Id == subscription.TenantId);

                    // Try to get subscription link
                    string subscriptionLink = await _azureSubscriptionService.GetSubscriptionLinkAsync(tenantSettings, subscription.CreationOperationUrl);

                    if (!string.IsNullOrEmpty(subscriptionLink))
                    {
                        subscription.State = SubscriptionState.Active;
                        subscription.SubscriptionId = subscriptionLink.Substring("/subscriptions/".Length);
                        await dbContext.SaveChangesAsync();
                        break;
                    }
                }
            }
        }
    }
}
