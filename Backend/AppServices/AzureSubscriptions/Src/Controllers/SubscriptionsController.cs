using CloudYourself.Backend.AppServices.AzureSubscriptions.Aggregates.Subscription;
using CloudYourself.Backend.AppServices.AzureSubscriptions.Dtos;
using CloudYourself.Backend.AppServices.AzureSubscriptions.Infrastructure;
using CloudYourself.Backend.AppServices.AzureSubscriptions.Services;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.AzureSubscriptions.Controllers
{
    /// <summary>
    /// A controller to manage azure subscriptions.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly AzureSubscriptionsDbContext _dbContext;

        /// <summary>
        /// The azure subscription service.
        /// </summary>
        private readonly AzureSubscriptionService _azureSubscriptionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionsController"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public SubscriptionsController(AzureSubscriptionsDbContext dbContext, AzureSubscriptionService azureSubscriptionService)
        {
            _dbContext = dbContext;
            _azureSubscriptionService = azureSubscriptionService;
        }

        /// <summary>
        /// Gets all subscriptions of a cloud account.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <returns>A list of subscriptions.</returns>
        [HttpGet]
        [Route("api/tenants/{tenantId}/cloud-accounts/{cloudAccountId}/azure-subscriptions")]
        public async Task<IActionResult> GetAllOfCloudAccount(int tenantId, int cloudAccountId)
        {
            List<Subscription> result = await _dbContext.Subscriptions.Where(azs => azs.CloudAccountId == cloudAccountId && azs.TenantId == tenantId).ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets a single subscription by its identifier.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <returns>A single subscription.</returns>
        [HttpGet]
        [Route("api/tenants/{tenantId}/cloud-accounts/{cloudAccountId}/azure-subscriptions/{subscriptionId}")]
        public async Task<IActionResult> GetById(int tenantId, int cloudAccountId, int subscriptionId)
        {
            Subscription result = await _dbContext.Subscriptions.SingleAsync(azs => azs.Id == subscriptionId && 
                                                                                    azs.CloudAccountId == cloudAccountId && 
                                                                                    azs.TenantId == tenantId);
            if(string.IsNullOrEmpty(result.SubscriptionLink))
            {
                result.SubscriptionLink = await _azureSubscriptionService.GetSubscriptionLinkAsync(tenantId, result.CreationOperationUrl);

                if(!string.IsNullOrEmpty(result.SubscriptionLink))
                {
                    result.State = SubscriptionState.Active;
                    await _dbContext.SaveChangesAsync();
                }   
            }

            return Ok(result);
        }

        /// <summary>
        /// Gets the template for a new subscription.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <returns>The subscription template</returns>
        [HttpGet]
        [Route("api/tenants/{tenantId}/cloud-accounts/{cloudAccountId}/azure-subscriptions/template")]
        public IActionResult GetNewTemplate(int tenantId, int cloudAccountId)
        {
            return Ok(new NewSubscriptionDto());
        }

        /// <summary>
        /// Creates a new subscription.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <param name="newSubscriptionDto">The new subscription dto.</param>
        /// <returns>The link to the new subscription.</returns>
        [HttpPost]
        [Route("api/tenants/{tenantId}/cloud-accounts/{cloudAccountId}/azure-subscriptions")]
        public async Task<IActionResult> Create(int tenantId, int cloudAccountId, [FromBody] NewSubscriptionDto newSubscriptionDto)
        {
            Subscription newSubscription = new Subscription { TenantId = tenantId, CloudAccountId = cloudAccountId };
            newSubscription.Name = newSubscriptionDto.Name;
            newSubscription.CreationOperationUrl = await _azureSubscriptionService.StartCreateSubscriptionOperationAsync(tenantId, newSubscription.Name);

            _dbContext.Subscriptions.Add(newSubscription);
            await _dbContext.SaveChangesAsync();

            return Created(Url.LinkTo<SubscriptionsController>(c => c.GetById(tenantId, cloudAccountId, newSubscription.Id)), newSubscription.Id);
        }

        /// <summary>
        /// Cancels a subscription by identifier.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        [HttpDelete]
        [Route("api/tenants/{tenantId}/cloud-accounts/{cloudAccountId}/azure-subscriptions/{subscriptionId}")]
        public async Task<IActionResult> CancelById(int tenantId, int cloudAccountId, int subscriptionId)
        {
            Subscription result = await _dbContext.Subscriptions.SingleAsync(azs => azs.Id == subscriptionId &&
                                                                                    azs.CloudAccountId == cloudAccountId &&
                                                                                    azs.TenantId == tenantId);
            if (string.IsNullOrEmpty(result.SubscriptionLink))
            {
                result.SubscriptionLink = await _azureSubscriptionService.GetSubscriptionLinkAsync(tenantId, result.CreationOperationUrl);
                await _dbContext.SaveChangesAsync();
            }

            await _azureSubscriptionService.CancelSubscriptionAsync(tenantId, result.SubscriptionLink);

            result.State = SubscriptionState.Cancelled;
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
