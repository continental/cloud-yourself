using CloudYourself.Backend.AppServices.Billing.Aggregates.AllocationKey;
using CloudYourself.Backend.AppServices.Billing.Aggregates.CloudAccount;
using CloudYourself.Backend.AppServices.Billing.Aggregates.Cost;
using CloudYourself.Backend.AppServices.Billing.Aggregates.PayerAccount;
using Microsoft.EntityFrameworkCore;

namespace CloudYourself.Backend.AppServices.Billing.Infrastructure
{
    /// <summary>
    /// The billing db context containing all aggregates.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class BillingDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BillingDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public BillingDbContext(DbContextOptions<BillingDbContext> options) : base(options)
        { }

        /// <summary>
        /// Gets or sets the payer accounts.
        /// </summary>
        /// <value>
        /// The payer accounts.
        /// </value>
        public DbSet<PayerAccount> PayerAccounts { get; set; }

        /// <summary>
        /// Gets or sets the cloud accounts.
        /// </summary>
        /// <value>
        /// The cloud accounts.
        /// </value>
        public DbSet<CloudAccount> CloudAccounts { get; set; }

        /// <summary>
        /// Gets or sets the allocation keys.
        /// </summary>
        /// <value>
        /// The allocation keys.
        /// </value>
        public DbSet<AllocationKey> AllocationKeys { get; set; }

        /// <summary>
        /// Gets or sets the costs.
        /// </summary>
        /// <value>
        /// The costs.
        /// </value>
        public DbSet<Cost> Costs { get; set; }

        /// <summary>
        /// Called by the contex to define th schema.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CloudAccount>().Property(t => t.Id).ValueGeneratedNever();

            modelBuilder.Entity<Cost>().OwnsOne(e => e.CostDetails);
            modelBuilder.Entity<PayerAccount>().OwnsOne(e => e.BaseData);
            modelBuilder.Entity<AllocationKey>().OwnsOne(e => e.BaseData);

            modelBuilder.Entity<Cost>().HasOne<CloudAccount>().WithMany().HasForeignKey(c => c.CloudAccountId);

            modelBuilder.Entity<AllocationKey>().HasOne<PayerAccount>().WithMany().HasForeignKey(ak => ak.PayerAccountId);
            modelBuilder.Entity<AllocationKey>().HasOne<CloudAccount>().WithMany().HasForeignKey(ak => ak.CloudAccountId);

        }
    }
}
