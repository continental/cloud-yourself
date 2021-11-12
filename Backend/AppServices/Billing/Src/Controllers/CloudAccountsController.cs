using CloudYourself.Backend.AppServices.Billing.Aggregates.Cost;
using CloudYourself.Backend.AppServices.Billing.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudYourself.Backend.AppServices.Billing.Controllers
{
    /// <summary>
    /// Controller to manage cloud accounts.
    /// </summary>
    /// <seealso cref="Fancy.ResourceLinker.HypermediaController" />
    [ApiController]
    public class CloudAccountsController : ControllerBase
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<CloudAccountsController> _logger;

        /// <summary>
        /// The database context.
        /// </summary>
        private readonly BillingDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudAccountsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        public CloudAccountsController(ILogger<CloudAccountsController> logger, BillingDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
    }
}
