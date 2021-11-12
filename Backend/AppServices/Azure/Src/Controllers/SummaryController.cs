using CloudYourself.Backend.AppServices.Azure.Dtos;
using CloudYourself.Backend.AppServices.Azure.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.Azure.Controllers
{
    /// <summary>
    /// Controller to provide summary data for azure items
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
        private readonly AzureDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SummaryController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        public SummaryController(ILogger<SummaryController> logger, AzureDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets the tenant related summary data.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns>
        /// The summary data.
        /// </returns>
        [HttpGet]
        [Route("api/azure/summary")]
        public async Task<IActionResult> Get(int tenantId)
        {
            SummaryDto result = new SummaryDto();
            result.ManagedResourcesCount = await _dbContext.ManagedResources.Where(mr => mr.TenantId == tenantId).CountAsync();
            return Ok(result);
        }
    }
}
