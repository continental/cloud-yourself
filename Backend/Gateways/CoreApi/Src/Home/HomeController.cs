using CloudYourself.Backend.Gateways.CoreApi.Infrastructure;
using Fancy.ResourceLinker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CloudYourself.Backend.Gateways.CoreApi.Home
{
    [ApiController]
    public class HomeController : ProxyController
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudAccountsProxyController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public HomeController(ILogger<HomeController> logger, IOptions<AppServiceBaseUrlOptions> appServiceBaseUrls)
            : base(appServiceBaseUrls.Value.CloudAccounts)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets the home view model.
        /// </summary>
        /// <returns>The home view model.</returns>
        [HttpGet]
        [Route("/api/home")]
        public async Task<IActionResult> Get()
        {
            HomeVm result = new HomeVm();
            result.DashboardInfos = await GetAsync<DynamicResource>(null, "/api/dashboard");
            return Hypermedia(result);
        }
    }
}
