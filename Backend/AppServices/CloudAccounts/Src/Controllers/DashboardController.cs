using CloudYourself.Backend.AppServices.CloudAccounts.Dtos;
using CloudYourself.Backend.AppServices.CloudAccounts.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.CloudAccounts.Controllers
{
    [ApiController]
    public class DashboardController : ControllerBase
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<DashboardController> _logger;

        /// <summary>
        /// The database context.
        /// </summary>
        private readonly CloudAccountsDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        public DashboardController(ILogger<DashboardController> logger, CloudAccountsDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets the dashbaord data.
        /// </summary>
        /// <returns>The dashboard data.</returns>
        [HttpGet]
        [Route("api/dashboard")]
        public async Task<IActionResult> Get()
        {
            DashboardDto result = new DashboardDto();
            result.TenantCount = await _dbContext.Tenants.CountAsync();
            result.CloudAccountCount = await _dbContext.CloudAccounts.CountAsync();
            return Ok(result);
        }
    }
}
