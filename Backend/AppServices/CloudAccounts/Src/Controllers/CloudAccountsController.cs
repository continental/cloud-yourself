using CloudYourself.Backend.AppServices.CloudAccounts.Aggregates.CloudAccount;
using CloudYourself.Backend.AppServices.CloudAccounts.Dtos;
using CloudYourself.Backend.AppServices.CloudAccounts.Infrastructure;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.CloudAccounts.Controllers
{
    /// <summary>
    /// Controller to manage cloud accounts.
    /// </summary>
    /// <seealso cref="Fancy.ResourceLinker.HypermediaController" />
    [ApiController]
    public class CloudAccountsController : HypermediaController
    {
        private readonly ILogger<CloudAccountsController> _logger;
        private readonly CloudAccountsDbContext _dbContext;

        public CloudAccountsController(ILogger<CloudAccountsController> logger, CloudAccountsDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("api/cloud-accounts")]
        public async Task<IActionResult> GetAll(CloudAccountState? state = null, int? tenantId = null)
        {
            IQueryable<CloudAccount> query = _dbContext.CloudAccounts;

            if(state.HasValue)
            {
                query = query.Where(ca => ca.State == state);
            }

            if(tenantId.HasValue)
            {
                query = query.Where(ca => ca.TenantId == tenantId);
            }

            List<CloudAccount> result = await query.ToListAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("api/cloud-accounts/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CloudAccount result = await _dbContext.CloudAccounts.SingleAsync(ca => ca.Id == id);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/cloud-accounts/request")]
        public IActionResult GetRequestTemplate()
        {
            CloudAccountRequestDto cloudAccountRequestDto = new CloudAccountRequestDto();
            cloudAccountRequestDto.BaseData = new CloudAccountBaseData();
            return Ok(cloudAccountRequestDto);
        }

        [HttpPost]
        [Route("api/cloud-accounts")]
        public async Task<IActionResult> Create([FromBody]CloudAccountRequestDto cloudAccountRequestDto)
        {
            CloudAccount newCloudAccount = new CloudAccount();
            newCloudAccount.TenantId = cloudAccountRequestDto.TenantId;
            newCloudAccount.BaseData = cloudAccountRequestDto.BaseData;

            _dbContext.CloudAccounts.Add(newCloudAccount);
            await _dbContext.SaveChangesAsync();

            return Created(Url.LinkTo<CloudAccountsController>(c => c.GetById(newCloudAccount.Id)), newCloudAccount.Id);
        }

        [HttpPost]
        [Route("/api/cloud-accounts/state")]
        public async Task<IActionResult> SetState(string newState, [FromBody] CloudAccount cloudAccount)
        {
            if(Enum.Parse<CloudAccountState>(newState) != CloudAccountState.Approved)
            {
                return BadRequest();
            }

            CloudAccount persistedAccount = await _dbContext.CloudAccounts.SingleAsync(ca => ca.Id == cloudAccount.Id);

            if(persistedAccount.State != CloudAccountState.Approved)
            {
                persistedAccount.State = CloudAccountState.Approved;
                await _dbContext.SaveChangesAsync();
                return NoContent();
            }

            return BadRequest();
        }

        /// <summary>
        /// Updates the base data of a cloud account.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="baseData">The base data.</param>
        [HttpPut]
        [Route("api/cloud-accounts/{id}/base-data")]
        public async Task<IActionResult> UpdateBaseData(int id, [FromBody] CloudAccountBaseData baseData)
        {
            CloudAccount cloudAccount = await _dbContext.CloudAccounts.SingleAsync(ca => ca.Id == id);
            cloudAccount.BaseData = baseData;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
