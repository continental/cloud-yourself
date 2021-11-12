using CloudYourself.Backend.AppServices.Azure.Aggregates.ManagedResource;
using CloudYourself.Backend.AppServices.Azure.Dtos;
using CloudYourself.Backend.AppServices.Azure.Infrastructure;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.Azure.Controllers
{
    /// <summary>
    /// A controller to manage azure managed resources.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiController]
    public class ManagedResourcesController : ControllerBase
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly AzureDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedResourcesController" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public ManagedResourcesController(AzureDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets a managed resource by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The managed resource.</returns>
        [HttpGet]
        [Route("api/azure/managed-resources/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ManagedResource result = await _dbContext.ManagedResources.SingleAsync(mr => mr.Id == id);
            return Ok(result);
        }

        /// <summary>
        /// Gets managed-resources filtered.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns>
        /// A list of managed-resources.
        /// </returns>
        [HttpGet]
        [Route("api/azure/managed-resources")]
        public async Task<IActionResult> GetFiltered(int tenantId)
        {
            IQueryable<ManagedResource> query = _dbContext.ManagedResources;

            query = query.Where(mr => mr.TenantId == tenantId);

            List<ManagedResource> result = await query.ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets the template for a new managed resource.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns>The managed resource template</returns>
        [HttpGet]
        [Route("api/azure/managed-resources/template")]
        public IActionResult GetNewTemplate(int tenantId)
        {
            return Ok(new NewManagedResourceDto { TenantId = tenantId });
        }

        /// <summary>
        /// Creates a new managed resource.
        /// </summary>
        /// <param name="managedResourceDto">The managed resource dto.</param>
        /// <returns>
        /// The link to the new managed resource.
        /// </returns>
        [HttpPost]
        [Route("api/azure/managed-resources")]
        public async Task<IActionResult> Create([FromBody] NewManagedResourceDto managedResourceDto)
        {
            ManagedResource newManagedResource = new ManagedResource();
            _dbContext.ManagedResources.Add(newManagedResource);

            newManagedResource.TenantId = managedResourceDto.TenantId;
            newManagedResource.BaseData = managedResourceDto.BaseData;
            newManagedResource.ComplianceSettings = managedResourceDto.ComplianceSettings;
            newManagedResource.ArmTemplate = managedResourceDto.ArmTemplate;

            await _dbContext.SaveChangesAsync();

            return Created(Url.LinkTo<ManagedResourcesController>(c => c.GetById(newManagedResource.Id)), newManagedResource.Id);
        }

        /// <summary>
        /// Deletes a managed resource by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete]
        [Route("api/azure/managed-resources/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            ManagedResource managedResource = await _dbContext.ManagedResources.SingleAsync(mr => mr.Id == id);
            _dbContext.ManagedResources.Remove(managedResource);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Updates the base data of a managed resource.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="baseData">The base data.</param>
        [HttpPut]
        [Route("api/azure/managed-resources/{id}/base-data")]
        public async Task<IActionResult> UpdateBaseData(int id, [FromBody] BaseData baseData)
        {
            ManagedResource managedResource = await _dbContext.ManagedResources.SingleAsync(mr => mr.Id == id);
            managedResource.BaseData = baseData;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Updates the compliance settings of a managed resource.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="complianceSettings">The compliance settings.</param>
        [HttpPut]
        [Route("api/azure/managed-resources/{id}/compliance-settings")]
        public async Task<IActionResult> UpdateComplianceSettings(int id, [FromBody] ComplianceSettings complianceSettings)
        {
            ManagedResource managedResource = await _dbContext.ManagedResources.SingleAsync(mr => mr.Id == id);
            managedResource.ComplianceSettings = complianceSettings;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Updates the arm template of a managed resource.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="baseData">The base data.</param>
        [HttpPut]
        [Route("api/azure/managed-resources/{id}/arm-template")]
        public async Task<IActionResult> UpdateArmTemplate(int id, [FromBody] ArmTemplate armTemplate)
        {
            ManagedResource managedResource = await _dbContext.ManagedResources.SingleAsync(mr => mr.Id == id);
            managedResource.ArmTemplate = armTemplate;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
