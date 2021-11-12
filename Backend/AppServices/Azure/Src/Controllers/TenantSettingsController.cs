using CloudYourself.Backend.AppServices.Azure.Aggregates.Tennant;
using CloudYourself.Backend.AppServices.Azure.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.Azure.Controllers
{
    /// <summary>
    /// A controller to manage tenant related information.
    /// </summary>
    [ApiController]
    public class TenantSettingsController : ControllerBase
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly AzureDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionsController"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public TenantSettingsController(AzureDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets settings by tenant identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The tenants settings.</returns>
        [HttpGet]
        [Route("api/azure/tenant-settings/{id}")]
        public async Task<IActionResult> GetByTenantId(int id)
        {
            TenantSettings result = await _dbContext.TenantSettings.SingleOrDefaultAsync(t => t.Id == id);
            return Ok(result ?? new TenantSettings());
        }

        /// <summary>
        /// Updates the app registration settings of a tenant.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="appRegistration">The application registration settings.</param>
        [HttpPut]
        [Route("api/azure/tenant-settings/{id}/app-registration")]
        public async Task<IActionResult> UpdateAppRegistration(int id, [FromBody] AppRegistration appRegistration)
        {
            TenantSettings tenant = await GetTenantAsync(id);

            tenant.AppRegistration = appRegistration;

            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Updates the management target settings of a tenant.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="managementTarget">The management target settings.</param>
        [HttpPut]
        [Route("api/azure/tenant-settings/{id}/management-target")]
        public async Task<IActionResult> UpdateManagementTarget(int id, [FromBody] ManagementTarget managementTarget)
        {
            TenantSettings tenant = await GetTenantAsync(id);

            tenant.ManagementTarget = managementTarget;

            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Gets a tenant asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns>A tenant.</returns>
        private async Task<TenantSettings> GetTenantAsync(int tenantId)
        {
            TenantSettings tenant = await _dbContext.TenantSettings.SingleOrDefaultAsync(t => t.Id == tenantId);

            if (tenant == null)
            {
                tenant = new TenantSettings();
                tenant.Id = tenantId;
                _dbContext.TenantSettings.Add(tenant);
            }

            return tenant;
        }
    }
}
