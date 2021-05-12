using CloudYourself.Backend.AppServices.CloudAccounts.Aggregates.Tenant;
using CloudYourself.Backend.AppServices.CloudAccounts.Dtos;
using CloudYourself.Backend.AppServices.CloudAccounts.Infrastructure;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.CloudAccounts.Controllers
{
    /// <summary>
    /// Controller to manage tenants for cloud accounts.
    /// </summary>
    /// <seealso cref="Fancy.ResourceLinker.HypermediaController" />
    [ApiController]
    public class TenantsController : HypermediaController
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<TenantsController> _logger;

        /// <summary>
        /// The database context.
        /// </summary>
        private readonly CloudAccountsDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        public TenantsController(ILogger<TenantsController> logger, CloudAccountsDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets all tenants.
        /// </summary>
        /// <returns>A list of tenants.</returns>
        [HttpGet]
        [Route("api/tenants")]
        public async Task<IActionResult> GetAll()
        {
            List<Tenant> result = await _dbContext.Tenants.OrderBy(t => t.BaseData.Name).ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets a tenant by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A tenant.</returns>
        [HttpGet]
        [Route("api/tenants/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Tenant result = await _dbContext.Tenants.SingleAsync(ca => ca.Id == id);
            return Ok(result);
        }

        /// <summary>
        /// Gets a template for a new tenant
        /// </summary>
        /// <returns>A new tenant template.</returns>
        [HttpGet]
        [Route("api/tenants/template")]
        public IActionResult GetTemplate()
        {
            NewTenantDto result = new NewTenantDto { BaseData = new TenantBaseData() };
            return Ok(result);
        }

        /// <summary>
        /// Creates a new tenant with the specified data.
        /// </summary>
        /// <param name="newTenantDto">The new tenant dto.</param>
        [HttpPost]
        [Route("api/tenants")]
        public async Task<IActionResult> Create([FromBody]NewTenantDto newTenantDto)
        {
            // Build the domain object
            Tenant newTenant = new Tenant();
            newTenant.BaseData = newTenantDto.BaseData;
            // Add it to database
            _dbContext.Tenants.Add(newTenant);
            await _dbContext.SaveChangesAsync();

            return Created(Url.LinkTo<TenantsController>(c => c.GetById(newTenant.Id)), newTenant.Id);
        }

        /// <summary>
        /// Updates the base data of a tenant.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="baseData">The base data.</param>
        [HttpPut]
        [Route("api/tenants/{id}/base-data")]
        public async Task<IActionResult> UpdateBaseData(int id, [FromBody] TenantBaseData baseData)
        {
            Tenant tenant = await _dbContext.Tenants.SingleAsync(ca => ca.Id == id);
            tenant.BaseData = baseData;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
