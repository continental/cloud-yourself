using CloudYourself.Backend.AppServices.Billing.Aggregates.Cost;
using CloudYourself.Backend.AppServices.Billing.Aggregates.PayerAccount;
using CloudYourself.Backend.AppServices.Billing.Dtos;
using CloudYourself.Backend.AppServices.Billing.Infrastructure;
using CloudYourself.Backend.AppServices.Billing.Services;
using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.Billing.Controllers
{
    /// <summary>
    /// Controller to manage payer accounts.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public class PayerAccountsController : ControllerBase
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<PayerAccountsController> _logger;

        /// <summary>
        /// The database context.
        /// </summary>
        private readonly BillingDbContext _dbContext;

        /// <summary>
        /// The cost query service.
        /// </summary>
        private readonly CostQueryService _costQueryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PayerAccountsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        public PayerAccountsController(ILogger<PayerAccountsController> logger, BillingDbContext dbContext, CostQueryService costQueryService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _costQueryService = costQueryService;
        }

        /// <summary>
        /// Gets a payer account by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A payer account.</returns>
        [HttpGet]
        [Route("api/billing/payer-accounts/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            PayerAccount result = await _dbContext.PayerAccounts.SingleAsync(pa => pa.Id == id);
            return Ok(result);
        }

        /// <summary>
        /// Gets payer accounts filtered.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns>
        /// A list of payer accounts.
        /// </returns>
        [HttpGet]
        [Route("api/billing/payer-accounts")]
        public async Task<IActionResult> GetFiltered(int tenantId)
        {
            IQueryable<PayerAccount> query = _dbContext.PayerAccounts;

            query = query.Where(pa => pa.TenantId == tenantId);

            List<PayerAccount> result = await query.ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets the cost summary of payer accounts.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns>A list of payer account cost summaries.</returns>
        [HttpGet]
        [Route("api/billing/payer-accounts/costs")]
        public async Task<IActionResult> GetCostSummaryOfPayerAccounts(int tenantId)
        {
            List<CostSummaryPayerAccountDto> result = await _costQueryService.GetCostSummaryOfPayerAccountsAsync(tenantId);
            return Ok(result);
        }

        /// <summary>
        /// Gets the cost summary of cloud accounts asynchronous.
        /// </summary>
        /// <param name="payerAccountId">The payer account identifier.</param>
        /// <param name="currency">The currency.</param>
        /// <returns>A list of cloud account cost summaries per payer account.</returns>
        [HttpGet]
        [Route("api/billing/payer-accounts/{payerAccountId}/cloud-accounts/costs")]
        public async Task<IActionResult> GetCostSummaryOfCloudAccount(int payerAccountId, Currency currency)
        {
            List<CostSummaryCloudAccountPerPayerAccountDto> result = await _costQueryService.GetCostSummaryOfCloudAccountsAsync(payerAccountId, currency);
            return Ok(result);
        }

        /// <summary>
        /// Gets the costs per allocation key asynchronous.
        /// </summary>
        /// <param name="payerAccountId">The payer account identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <param name="currency">The currency.</param>
        /// <returns>A list of cost items with share for a specific payer account.</returns>
        [HttpGet]
        [Route("api/billing/payer-accounts/{payerAccountId}/cloud-accounts/{cloudAccountId}/costs")]
        public async Task<IActionResult> GetCostsPerAllocationKey(int payerAccountId, int cloudAccountId, Currency currency)
        {
            var result = await _costQueryService.GetCostsPerAllocationKeyAsync(payerAccountId, cloudAccountId, currency);
            return Ok(result);
        }

        /// <summary>
        /// Gets the template for a new payer account.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns>The payer account template</returns>
        [HttpGet]
        [Route("api/billing/payer-accounts/template")]
        public IActionResult GetNewTemplate(int tenantId)
        {
            return Ok(new NewPayerAccountDto { TenantId = tenantId });
        }

        /// <summary>
        /// Creates a new payer account.
        /// </summary>
        /// <param name="payerAccountDto">The payer account dto.</param>
        /// <returns>
        /// The link to the new payer account.
        /// </returns>
        [HttpPost]
        [Route("api/billing/payer-accounts")]
        public async Task<IActionResult> Create([FromBody] NewPayerAccountDto payerAccountDto)
        {
            PayerAccount newPayerAccount = new PayerAccount();
            _dbContext.PayerAccounts.Add(newPayerAccount);

            newPayerAccount.TenantId = payerAccountDto.TenantId;
            newPayerAccount.BaseData = payerAccountDto.BaseData;

            await _dbContext.SaveChangesAsync();

            return Created(Url.LinkTo<PayerAccountsController>(c => c.GetById(newPayerAccount.Id)), newPayerAccount.Id);
        }

        /// <summary>
        /// Deletes a payer account by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete]
        [Route("api/billing/payer-accounts/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            PayerAccount payerAccount = await _dbContext.PayerAccounts.SingleAsync(pa => pa.Id == id);

            // ToDo: Make sure, payer account is not assigned to any allocation keys.

            _dbContext.PayerAccounts.Remove(payerAccount);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Updates the base data of a payer account.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="baseData">The base data.</param>
        [HttpPut]
        [Route("api/billing/payer-accounts/{id}/base-data")]
        public async Task<IActionResult> UpdateBaseData(int id, [FromBody] PayerAccountBaseData baseData)
        {
            PayerAccount payerAccount = await _dbContext.PayerAccounts.SingleAsync(pa => pa.Id == id);
            payerAccount.BaseData = baseData;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
