using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudYourself.Backend.Gateways.AutomationApi.Root
{
    /// <summary>
    /// A Controller to get the root of the api with links to possible HATEOAS state transitions.
    /// </summary>
    [Authorize]
    [ApiController]
    public class RootController : HypermediaController
    {
        /// <summary>
        /// Gets the root information of the web api.
        /// </summary>
        /// <returns>View model with root information.</returns>
        [HttpGet]
        [Route("/api/root")]
        public IActionResult Get()
        {
            return Hypermedia(new RootDto());
        }
    }
}
