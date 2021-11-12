using CloudYourself.Backend.AppServices.MasterData.Dtos;
using CloudYourself.Backend.AppServices.MasterData.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.MasterData.Controllers
{
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
        private readonly MasterDataDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SummaryController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        public SummaryController(ILogger<SummaryController> logger, MasterDataDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets the dashbaord data.
        /// </summary>
        /// <returns>The dashboard data.</returns>
        [HttpGet]
        [Route("api/master-data/summary")]
        public async Task<IActionResult> Get()
        {
            SummaryDto result = new SummaryDto();
            result.TenantCount = await _dbContext.Tenants.CountAsync();
            result.CloudAccountCount = await _dbContext.CloudAccounts.CountAsync();
            return Ok(result);
        }
    }
}
