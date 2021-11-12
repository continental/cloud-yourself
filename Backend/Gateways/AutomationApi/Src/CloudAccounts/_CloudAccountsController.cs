using CloudYourself.Backend.Gateways.AutomationApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace CloudYourself.Backend.Gateways.AutomationApi.CloudAccounts
{
    /// <summary>
    /// A controller to proxy requests to the cloud accounts.
    /// </summary>
    /// <seealso cref="ProxyController" />
    [Authorize]
    [ApiController]
    public class CloudAccountsController : ProxyController
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<CloudAccountsController> _logger;

        /// <summary>
        /// The application service base urls.
        /// </summary>
        private readonly IOptions<AppServiceBaseUrlOptions> _appServiceBaseUrls;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudAccountsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public CloudAccountsController(ILogger<CloudAccountsController> logger, IOptions<AppServiceBaseUrlOptions> appServiceBaseUrls)
            : base(appServiceBaseUrls.Value.MasterData)
        {
            _logger = logger;
            _appServiceBaseUrls = appServiceBaseUrls;
        }

        /// <summary>
        /// Gets all cloud accounts.
        /// </summary>
        /// <returns>The cloud accounts.</returns>
        [HttpGet]
        [Route("/api/cloud-accounts")]
        public async Task<IActionResult> GetAll()
        {
            List<CloudAccountDto> result = await GetCollectionAsync<CloudAccountDto>(null, $"/api/master-data/cloud-accounts");
            return Hypermedia(result);
        }

        /// <summary>
        /// Gets a specific cloud account identified by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A cloud account.</returns>
        [HttpGet]
        [Route("/api/cloud-accounts/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CloudAccountDto result = await GetAsync<CloudAccountDto>(null, $"/api/master-data/cloud-accounts/{id}");
            return Hypermedia(result);
        }
    }
}
