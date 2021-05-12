using CloudYourself.Backend.AppServices.AzureSubscriptions.Aggregates.Subscription;
using CloudYourself.Backend.AppServices.AzureSubscriptions.Aggregates.Tennant;
using Microsoft.EntityFrameworkCore;

namespace CloudYourself.Backend.AppServices.AzureSubscriptions.Infrastructure
{
    /// <summary>
    /// The azure subscriptions db context containing all aggregates.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class AzureSubscriptionsDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureSubscriptionsDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AzureSubscriptionsDbContext(DbContextOptions<AzureSubscriptionsDbContext> options) 
            : base(options) { }

        /// <summary>
        /// Gets or sets the tenants.
        /// </summary>
        /// <value>
        /// The tenants.
        /// </value>
        public DbSet<Tenant> Tenants { get; set; }

        /// <summary>
        /// Gets or sets the azure subscriptions.
        /// </summary>
        /// <value>
        /// The azure subscriptions.
        /// </value>
        public DbSet<Subscription> Subscriptions { get; set; }

        /// <summary>
        /// Called by the context to define th schema.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tenant>().Property(t => t.Id).ValueGeneratedNever();
            modelBuilder.Entity<Tenant>().OwnsOne(t => t.AppRegistration);
            modelBuilder.Entity<Tenant>().OwnsOne(t => t.ManagementTarget);

            modelBuilder.Entity<Subscription>().HasOne<Tenant>().WithMany().HasForeignKey(ca => ca.TenantId);
        }
    }
}
