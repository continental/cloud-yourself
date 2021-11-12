using Amazon.Organizations;
using Amazon.Organizations.Model;
using Amazon.Runtime.CredentialManagement;
using CloudYourself.Backend.AppServices.Aws.Aggregates.TennantSettings;
using CloudYourself.Backend.AppServices.Aws.Dtos;
using CloudYourself.Backend.AppServices.Aws.Infrastructure;
using Fancy.ResourceLinker;
using Fancy.ResourceLinker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account = CloudYourself.Backend.AppServices.Aws.Aggregates.Account.Account;

namespace CloudYourself.Backend.AppServices.Aws.Controllers
{
    /// <summary>
    /// A controller to manage aws accounts.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiController]
    public class AccountsController : ControllerBase
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly AwsDbContext _dbContext;

        /// <summary>
        /// The database context options.
        /// </summary>
        private readonly DbContextOptions<AwsDbContext> _dbContextOptions;

        public AccountsController(AwsDbContext dbContext, DbContextOptions<AwsDbContext> dbContextOptions)
        {
            _dbContext = dbContext;
            _dbContextOptions = dbContextOptions;
        }

        /// <summary>
        /// Gets an account by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpGet]
        [Route("api/aws/accounts/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Account result = await _dbContext.Accounts.SingleAsync(azs => azs.Id == id);
            return Ok(result);
        }

        /// <summary>
        /// Gets accounts filtered.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <returns>
        /// A list of accounts.
        /// </returns>
        [HttpGet]
        [Route("api/aws/accounts")]
        public async Task<IActionResult> GetFiltered(int? tenantId = null, int? cloudAccountId = null)
        {
            IQueryable<Account> query = _dbContext.Accounts;

            if(tenantId.HasValue)
            {
                query = query.Where(azs => azs.TenantId == tenantId.Value);
            }

            if (cloudAccountId.HasValue)
            {
                query = query.Where(azs => azs.CloudAccountId == cloudAccountId.Value);
            }

            List<Account> result = await query.ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets all unmanaged accounts of a tenant.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        [HttpGet]
        [Route("api/aws/accounts/unmanaged")]
        public async Task<IActionResult> GetAllUnmanaged(int tenantId)
        {
            TenantSettings tenantSettings = await _dbContext.TenantSettings.SingleAsync(ts => ts.Id == tenantId);
            SetUpProfile(tenantSettings.IamAccount.AccessKeyId, tenantSettings.IamAccount.Secret);

            // Get AWS Accounts
            AmazonOrganizationsClient organizationsClient = new AmazonOrganizationsClient();
            ListAccountsResponse accountResponse = await organizationsClient.ListAccountsAsync(new ListAccountsRequest());

            List<UnmanagedAccountDto> result = new List<UnmanagedAccountDto>();

            foreach (Amazon.Organizations.Model.Account account in accountResponse.Accounts)
            {
                result.Add(new UnmanagedAccountDto { Name = account.Name, AwsAccountId = account.Id, TenantId = tenantId });
            }

            return Ok(result);
        }

        /// <summary>
        /// Gets the template for a new account.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cloudAccountId">The cloud account identifier.</param>
        /// <returns>The account template</returns>
        [HttpGet]
        [Route("api/aws/accounts/template")]
        public IActionResult GetNewTemplate(int tenantId, int cloudAccountId)
        {
            return Ok(new NewAccountDto { TenantId = tenantId, CloudAccountId = cloudAccountId });
        }

        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <param name="accountDto">The account dto.</param>
        /// <returns>
        /// The link to the new account.
        /// </returns>
        [HttpPost]
        [Route("api/aws/accounts")]
        public async Task<IActionResult> Create([FromBody] DynamicResource accountDto)
        {
            Account newAccount = new Account();
            _dbContext.Accounts.Add(newAccount);

            newAccount.TenantId = Convert.ToInt32(accountDto["TenantId"]);
            newAccount.CloudAccountId = Convert.ToInt32(accountDto["CloudAccountId"]);

            TenantSettings tenantSettings = await _dbContext.TenantSettings.SingleAsync(ts => ts.Id == newAccount.TenantId);

            if (accountDto.ContainsKey("AwsAccountId"))
            {
                // Add an existing account
                newAccount.Name = accountDto["Name"].ToString();
                newAccount.AwsAccountId = accountDto["AwsAccountId"].ToString();
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                // Create a new account
                throw new NotImplementedException();
            }

            return Created(Url.LinkTo<AccountsController>(c => c.GetById(newAccount.Id)), newAccount.Id);
        }

        /// <summary>
        /// Sets up profile.
        /// </summary>
        /// <param name="keyId">The key identifier.</param>
        /// <param name="secret">The secret.</param>
        private void SetUpProfile(string keyId, string secret)
        {
            Console.WriteLine($"Creating profile...");
            var options = new CredentialProfileOptions
            {
                AccessKey = keyId,
                SecretKey = secret
            };
            var profile = new CredentialProfile("AwsCloudYourself-Profile", options);
            var sharedFile = new SharedCredentialsFile();
            sharedFile.RegisterProfile(profile);

            Environment.SetEnvironmentVariable("AWS_PROFILE", "AwsSpike-Profile");
            Environment.SetEnvironmentVariable("AWS_REGION", "eu-west-1");
        }
    }
}
