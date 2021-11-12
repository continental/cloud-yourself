using CloudYourself.Backend.AppServices.Aws.Aggregates.Account;
using CloudYourself.Backend.AppServices.Aws.Aggregates.TennantSettings;
using Microsoft.EntityFrameworkCore;

namespace CloudYourself.Backend.AppServices.Aws.Infrastructure
{
    /// <summary>
    /// The azure subscriptions db context containing all aggregates.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class AwsDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AwsDbContext(DbContextOptions<AwsDbContext> options) 
            : base(options) { }

        /// <summary>
        /// Gets or sets the tenants.
        /// </summary>
        /// <value>
        /// The tenants.
        /// </value>
        public DbSet<TenantSettings> TenantSettings { get; set; }

        /// <summary>
        /// Gets or sets the accounts.
        /// </summary>
        /// <value>
        /// The accounts.
        /// </value>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Called by the context to define th schema.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TenantSettings>().Property(t => t.Id).ValueGeneratedNever();
            modelBuilder.Entity<TenantSettings>().OwnsOne(t => t.IamAccount);
        }
    }
}
