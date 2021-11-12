using CloudYourself.Backend.Gateways.SelfServicePortalApi.Infrastructure;
using Fancy.ResourceLinker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.Views.Home
{
    [Authorize]
    [ApiController]
    public class HomeViewsController : ProxyController
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<HomeViewsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeViewsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public HomeViewsController(ILogger<HomeViewsController> logger, IOptions<AppServiceBaseUrlOptions> appServiceBaseUrls)
            : base(appServiceBaseUrls.Value.MasterData)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets the home view model.
        /// </summary>
        /// <returns>The home view model.</returns>
        [HttpGet]
        [Route("/api/views/home")]
        public async Task<IActionResult> Get()
        {
            HomeVm result = new HomeVm();
            result.DashboardInfos = await GetAsync<DynamicResource>(null, "/api/master-data/summary");
            return Hypermedia(result);
        }
    }
}
