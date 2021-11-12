using CloudYourself.Backend.AppServices.Billing.Aggregates.Cost;
using CloudYourself.Backend.AppServices.Billing.Aggregates.PayerAccount;
using CloudYourself.Backend.AppServices.Billing.Dtos;
using CloudYourself.Backend.AppServices.Billing.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.Billing.Services
{
    /// <summary>
    /// A service to query costs in different ways.
    /// </summary>
    public class CostQueryService
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly BillingDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CostQueryService"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public CostQueryService(BillingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets the cost summary of payer accounts in an tenant asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns>A list of payer account cost summaries.</returns>
        public async Task<List<CostSummaryPayerAccountDto>> GetCostSummaryOfPayerAccountsAsync(int tenantId)
        {
            IQueryable<PayerAccount> payerAccounts = _dbContext.PayerAccounts;

            payerAccounts = payerAccounts.Where(pa => pa.TenantId == tenantId);

            var payerAccountCosts = from payerAccount in payerAccounts
                                    join allocationKey in _dbContext.AllocationKeys on payerAccount.Id equals allocationKey.PayerAccountId
                                    join cost in _dbContext.Costs on allocationKey.CloudAccountId equals cost.CloudAccountId
                                    group new
                                    {
                                        PayerAccountId = payerAccount.Id,
                                        CostCenter = payerAccount.BaseData.CostCenter,
                                        Currency = cost.CostDetails.Currency,
                                        AllocationAmount = cost.CostDetails.Amount * (allocationKey.BaseData.AllocationPercentage / 100.0)
                                    } by new
                                    {
                                        PayerAccountId = payerAccount.Id,
                                        CostCenter = payerAccount.BaseData.CostCenter,
                                        Currency = cost.CostDetails.Currency
                                    } into g
                                    select new { PayerAccountId = g.Key.PayerAccountId, CostCenter = g.Key.CostCenter, Currency = g.Key.Currency, AllocationAmountSum = g.Sum(x => x.AllocationAmount) };

            var queryResult = await payerAccountCosts.ToListAsync();

            // Round allocation sum to tow decimals
            List<CostSummaryPayerAccountDto> result = queryResult.Select(pa => new CostSummaryPayerAccountDto
            {
                PayerAccountId = pa.PayerAccountId,
                CostCenter = pa.CostCenter,
                Currency = pa.Currency,
                AllocationAmountSum = (float)Math.Round(pa.AllocationAmountSum, 2)
            }).ToList();

            return result;
        }

        /// <summary>
        /// Gets the cost summary of cloud accounts asynchronous.
        /// </summary>
        /// <param name="payerAccountId">The payer account identifier.</param>
        /// <param name="currency">The currency.</param>
        /// <returns>A list of cloud account cost summaries per payer account.</returns>
        public async Task<List<CostSummaryCloudAccountPerPayerAccountDto>> GetCostSummaryOfCloudAccountsAsync(int payerAccountId, Currency currency)
        {
            var cloudAccountCosts = from allocationKey in _dbContext.AllocationKeys
                                    join cost in _dbContext.Costs on allocationKey.CloudAccountId equals cost.CloudAccountId
                                    where allocationKey.PayerAccountId == payerAccountId && cost.CostDetails.Currency == currency
                                    group new
                                    {
                                        CloudAccountId = cost.CloudAccountId,
                                        AllocationAmount = cost.CostDetails.Amount * (allocationKey.BaseData.AllocationPercentage / 100.0)
                                    } by new
                                    {
                                        CloudAccountId = cost.CloudAccountId
                                    } into g
                                    select new { CloudAccountId = g.Key.CloudAccountId, Currency = currency, AllocationAmountSum = g.Sum(x => x.AllocationAmount) };

            var queryResult = await cloudAccountCosts.ToListAsync();

            // Round allocation sum to tow decimals
            List<CostSummaryCloudAccountPerPayerAccountDto> result = queryResult.Select(pa => new CostSummaryCloudAccountPerPayerAccountDto
            {
                PayerAccountId = payerAccountId,
                CloudAccountId = pa.CloudAccountId,
                Currency = currency,
                AllocationAmountSum = (float)Math.Round(pa.AllocationAmountSum, 2)
            }).ToList();

            return result;
        }

        /// <summary>
        /// Gets the costs per allocation key asynchronous.
        /// </summary>
        /// <param name="payerAccountId">The payer account identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <param name="currency">The currency.</param>
        /// <returns>A list of cost items with share for a specific payer account.</returns>
        public async Task<List<CostPerAllocationKeyDto>> GetCostsPerAllocationKeyAsync(int payerAccountId, int cloudAccountId, Currency currency)
        {
            IQueryable<PayerAccount> payerAccounts = _dbContext.PayerAccounts;

            var costs = from allocationKey in _dbContext.AllocationKeys
                        join cost in _dbContext.Costs on allocationKey.CloudAccountId equals cost.CloudAccountId
                        where allocationKey.PayerAccountId == payerAccountId && cost.CloudAccountId == cloudAccountId && cost.CostDetails.Currency == currency
                        orderby cost.CostId, cost.CostDetails.PeriodId
                        select new
                        {
                            CloudAccountId = cost.CloudAccountId,
                            CostType = cost.CostType,
                            CostId = cost.CostId,
                            PeriodId = cost.CostDetails.PeriodId,
                            Currency = currency,
                            TotalAmount = cost.CostDetails.Amount,
                            AllocationPercentage = allocationKey.BaseData.AllocationPercentage
                        };

            var queryResult = await costs.ToListAsync();

            // Round allocation sum to tow decimals
            List<CostPerAllocationKeyDto> result = queryResult.Select(c => new CostPerAllocationKeyDto
            {
                CloudAccountId = cloudAccountId,
                CostType = c.CostType,
                CostId = c.CostId,
                PeriodId = c.PeriodId,
                Currency = currency,
                TotalAmount = (float)Math.Round(c.TotalAmount, 2),
                AllocationPercentage = c.AllocationPercentage,
                AllocationAmount = (float)Math.Round(c.TotalAmount * (c.AllocationPercentage / 100.0f), 2)
            }).ToList();

            return result;
        }
    }
}
