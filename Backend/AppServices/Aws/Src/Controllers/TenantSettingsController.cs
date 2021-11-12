using CloudYourself.Backend.AppServices.Aws.Aggregates.TennantSettings;
using CloudYourself.Backend.AppServices.Aws.Infrastructure;
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
        private readonly AwsDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantSettingsController"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public TenantSettingsController(AwsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets settings by tenant identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The tenants settings.</returns>
        [HttpGet]
        [Route("api/aws/tenant-settings/{id}")]
        public async Task<IActionResult> GetByTenantId(int id)
        {
            TenantSettings result = await _dbContext.TenantSettings.SingleOrDefaultAsync(t => t.Id == id);
            return Ok(result ?? new TenantSettings());
        }

        /// <summary>
        /// Updates the iam account for a tenant.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="iamAccount">The iam account.</param>
        [HttpPut]
        [Route("api/aws/tenant-settings/{id}/iam-account")]
        public async Task<IActionResult> UpdateAppRegistration(int id, [FromBody] IamAccount iamAccount)
        {
            TenantSettings tenant = await GetTenantAsync(id);

            tenant.IamAccount = iamAccount;

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
