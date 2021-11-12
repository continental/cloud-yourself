using CloudYourself.Backend.AppServices.Billing.Aggregates.AllocationKey;
using CloudYourself.Backend.AppServices.Billing.Aggregates.CloudAccount;
using CloudYourself.Backend.AppServices.Billing.Dtos;
using CloudYourself.Backend.AppServices.Billing.Infrastructure;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.Billing.Controllers
{
    /// <summary>
    /// Controller to manage allocation keys.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public class AllocationKeysController : ControllerBase
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<AllocationKeysController> _logger;

        /// <summary>
        /// The database context.
        /// </summary>
        private readonly BillingDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllocationKeysController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        public AllocationKeysController(ILogger<AllocationKeysController> logger, BillingDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets an allocation key by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A allocation key.</returns>
        [HttpGet]
        [Route("api/billing/allocation-keys/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            AllocationKey result = await _dbContext.AllocationKeys.SingleAsync(ak => ak.Id == id);
            return Ok(result);
        }

        /// <summary>
        /// Gets allocation keys filtered.
        /// </summary>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <returns>
        /// A list of allocation keys.
        /// </returns>
        [HttpGet]
        [Route("api/billing/allocation-keys")]
        public async Task<IActionResult> GetFiltered(int cloudAccountId)
        {
            IQueryable<AllocationKey> query = _dbContext.AllocationKeys;

            query = query.Where(ak => ak.CloudAccountId == cloudAccountId);

            List<AllocationKey> result = await query.ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets the template for a new allocation key.
        /// </summary>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <returns>
        /// The allocation key template
        /// </returns>
        [HttpGet]
        [Route("api/billing/allocation-keys/template")]
        public IActionResult GetNewTemplate(int cloudAccountId)
        {
            return Ok(new NewAllocationKeyDto { CloudAccountId = cloudAccountId });
        }

        /// <summary>
        /// Creates a new allocation key.
        /// </summary>
        /// <param name="allocationKeyDto">The new allocation key dto.</param>
        /// <returns>
        /// The link to the new allocation key.
        /// </returns>
        [HttpPost]
        [Route("api/billing/allocation-keys")]
        public async Task<IActionResult> Create([FromBody] NewAllocationKeyDto allocationKeyDto)
        {
            AllocationKey newAllocationKey = new AllocationKey();
            _dbContext.AllocationKeys.Add(newAllocationKey);

            newAllocationKey.CloudAccountId = allocationKeyDto.CloudAccountId;
            newAllocationKey.PayerAccountId = allocationKeyDto.PayerAccountId;
            newAllocationKey.BaseData = allocationKeyDto.BaseData;

            if(! await _dbContext.CloudAccounts.AnyAsync(ca => ca.Id == allocationKeyDto.CloudAccountId))
            {
                // Create a new cloud account
                CloudAccount newCloudAccount = new CloudAccount() { Id = allocationKeyDto.CloudAccountId };
                _dbContext.CloudAccounts.Add(newCloudAccount);
            }

            await _dbContext.SaveChangesAsync();

            return Created(Url.LinkTo<AllocationKeysController>(c => c.GetById(newAllocationKey.Id)), newAllocationKey.Id);
        }

        /// <summary>
        /// Deletes an allocation key by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete]
        [Route("api/billing/allocation-keys/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            AllocationKey allocationKey = await _dbContext.AllocationKeys.SingleAsync(ak => ak.Id == id);
            _dbContext.AllocationKeys.Remove(allocationKey);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Updates the base data of an allocation key.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="baseData">The base data.</param>
        [HttpPut]
        [Route("api/billing/allocation-keys/{id}/base-data")]
        public async Task<IActionResult> UpdateBaseData(int id, [FromBody] AllocationKeyBaseData baseData)
        {
            AllocationKey allocationKey = await _dbContext.AllocationKeys.SingleAsync(ak => ak.Id == id);
            allocationKey.BaseData = baseData;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
