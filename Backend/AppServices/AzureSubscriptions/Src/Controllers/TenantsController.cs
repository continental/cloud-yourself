using CloudYourself.Backend.AppServices.AzureSubscriptions.Aggregates.Tennant;
using CloudYourself.Backend.AppServices.AzureSubscriptions.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.AzureSubscriptions.Controllers
{
    /// <summary>
    /// A controller to manage tenant related information.
    /// </summary>
    [ApiController]
    public class TenantsController : ControllerBase
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly AzureSubscriptionsDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionsController"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public TenantsController(AzureSubscriptionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets settings by tenant identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The tenants settings.</returns>
        [HttpGet]
        [Route("api/tenants/{id}/azure-subscriptions")]
        public async Task<IActionResult> GetByTenantId(int id)
        {
            Tenant result = await _dbContext.Tenants.SingleOrDefaultAsync(t => t.Id == id);
            return Ok(result ?? new Tenant());
        }

        /// <summary>
        /// Updates the app registration of a tenant.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="appRegistration">The application registration.</param>
        [HttpPut]
        [Route("api/tenants/{id}/azure-subscriptions/app-registration")]
        public async Task<IActionResult> UpdateAppRegistration(int id, [FromBody] AppRegistration appRegistration)
        {
            Tenant tenant = await GetTenantAsync(id);

            tenant.AppRegistration = appRegistration;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Updates the management target of a tenant.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="managementTarget">The management target.</param>
        [HttpPut]
        [Route("api/tenants/{id}/azure-subscriptions/management-target")]
        public async Task<IActionResult> UpdateManagementTarget(int id, [FromBody] ManagementTarget managementTarget)
        {
            Tenant tenant = await GetTenantAsync(id);

            tenant.ManagementTarget = managementTarget;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Gets a tenant asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns>A tenant.</returns>
        private async Task<Tenant> GetTenantAsync(int tenantId)
        {
            Tenant tenant = await _dbContext.Tenants.SingleOrDefaultAsync(t => t.Id == tenantId);

            if (tenant == null)
            {
                tenant = new Tenant();
                tenant.Id = tenantId;
                _dbContext.Tenants.Add(tenant);
            }

            return tenant;
        }
    }
}
