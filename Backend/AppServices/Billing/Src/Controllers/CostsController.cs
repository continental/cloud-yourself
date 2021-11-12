using CloudYourself.Backend.AppServices.Billing.Aggregates.CloudAccount;
using CloudYourself.Backend.AppServices.Billing.Aggregates.Cost;
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
    /// Controller to manage costs.
    /// </summary>
    /// <seealso cref="Fancy.ResourceLinker.HypermediaController" />
    [ApiController]
    public class CostsController : ControllerBase
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<CostsController> _logger;

        /// <summary>
        /// The database context.
        /// </summary>
        private readonly BillingDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CostsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        public CostsController(ILogger<CostsController> logger, BillingDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets the new cost template.
        /// </summary>
        /// <param name="costType">Type of the cost.</param>
        /// <param name="costId">The cost identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/billing/costs/{costType}/{costId}/template")]
        public IActionResult GetNewTemplate(CostType costType, string costId, int cloudAccountId, int tenantId)
        {
            NewCostDto createCostTemplate = new NewCostDto { CostType = costType, CostId = costId, CloudAccountId = cloudAccountId, TenantId = tenantId};
            return Ok(createCostTemplate);
        }

        /// <summary>
        /// Creates a new cost asynchronous.
        /// </summary>
        /// <param name="createCostDto">The create cost dto.</param>
        [HttpPost]
        [Route("api/billing/costs")]
        public async Task<IActionResult> CreateNewAsync([FromBody] NewCostDto createCostDto)
        {
            Cost newCost = new Cost
            {
                CostType = createCostDto.CostType,
                CostId = createCostDto.CostId,
                CloudAccountId = createCostDto.CloudAccountId,
                TenantId = createCostDto.TenantId,
                CostDetails = createCostDto.CostDetails 
            };

            if (!await _dbContext.CloudAccounts.AnyAsync(ca => ca.Id == createCostDto.CloudAccountId))
            {
                // Create a new cloud account
                CloudAccount newCloudAccount = new CloudAccount() { Id = createCostDto.CloudAccountId };
                _dbContext.CloudAccounts.Add(newCloudAccount);
            }

            _dbContext.Costs.Add(newCost);
            await _dbContext.SaveChangesAsync();

            // ToDo: Find out error with linking
            return Created(/*Url.LinkTo<CostsController>(c => c.GetByIdAsync(newCost.Id))*/ "", newCost.Id);
        }

        /// <summary>
        /// Gets the cost by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpGet]
        [Route("api/billing/costs/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Cost cost = await _dbContext.Costs.SingleOrDefaultAsync(c => c.Id == id);

            if (cost != null) return Ok(cost);
            else return NotFound();
        }

        /// <summary>
        /// Gets a specific cost of a specific type with a specific id of a specific period id asynchronous.
        /// </summary>
        /// <param name="costType">Type of the cost.</param>
        /// <param name="costId">The cost identifier.</param>
        /// <param name="periodId">The period identifier.</param>
        [HttpGet]
        [Route("api/billing/costs/{costType}/{costId}/{periodId}")]
        public async Task<IActionResult> GetByPeriodAsync(CostType costType, string costId, string periodId)
        {
            Cost cost = await _dbContext.Costs.SingleOrDefaultAsync(c => c.CostType == costType && c.CostId == costId && c.CostDetails.PeriodId == periodId);

            if (cost != null) return Ok(cost);
            else return NoContent();
        }

        /// <summary>
        /// Gets the costs of a specified cost id asynchronous.
        /// </summary>
        /// <param name="costType">Type of the cost.</param>
        /// <param name="costId">The cost identifier.</param>
        /// <param name="periodId">The period identifier.</param>
        [HttpGet]
        [Route("api/billing/costs/{costType}/{costId}")]
        public async Task<IActionResult> GetByCostIdAsync(CostType costType, string costId)
        {
            if (HttpContext.Request.Query.ContainsKey("periodId"))
            {
                string periodId = HttpContext.Request.Query["periodId"];
                Cost cost = await _dbContext.Costs.SingleOrDefaultAsync(c => c.CostType == costType && c.CostId == costId && c.CostDetails.PeriodId == periodId);

                if (cost != null) return Ok(cost);
                else return NoContent();
            }
            else
            {
                List<Cost> costs = await _dbContext.Costs
                                                   .Where(c => c.CostType == costType && c.CostId == costId)
                                                   .OrderByDescending(c => c.CostDetails.PeriodEnd)
                                                   .Take(10)
                                                   .ToListAsync();
                return Ok(costs);
            }
        }

        /// <summary>
        /// Updates the cost details asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="costDetails">The cost details.</param>
        [HttpPut]
        [Route("api/billing/costs/{id}")]
        public async Task<IActionResult> UpdateCostDetailsAsync(int id, [FromBody] CostDetails costDetails)
        {
            Cost cost = _dbContext.Costs.Single(c => c.Id == id);

            // Update cost entry
            cost.CostDetails = costDetails;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
