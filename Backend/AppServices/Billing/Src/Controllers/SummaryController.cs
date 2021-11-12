using CloudYourself.Backend.AppServices.Billing.Dtos;
using CloudYourself.Backend.AppServices.Billing.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.Billing.Controllers
{
    /// <summary>
    /// Controller to provide summary data for billing items
    /// </summary>
    [ApiController]
    public class SummaryController : ControllerBase
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<SummaryController> _logger;

        /// <summary>
        /// The database context.
        /// </summary>
        private readonly BillingDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SummaryController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        public SummaryController(ILogger<SummaryController> logger, BillingDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets the dashbaord data.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns>
        /// The dashboard data.
        /// </returns>
        [HttpGet]
        [Route("api/billing/summary")]
        public async Task<IActionResult> Get(int tenantId)
        {
            SummaryDto result = new SummaryDto();
            result.PayerAccountsCount = await _dbContext.PayerAccounts.Where(pa => pa.TenantId == tenantId).CountAsync();
            result.CostItemsCount = await _dbContext.Costs.Where(c => c.TenantId == tenantId).CountAsync();
            return Ok(result);
        }
    }
}
