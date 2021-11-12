using CloudYourself.Backend.AppServices.Azure.Aggregates.ManagedResource;
using CloudYourself.Backend.AppServices.Azure.Aggregates.ManagedResourceDeployment;
using CloudYourself.Backend.AppServices.Azure.Aggregates.Subscription;
using CloudYourself.Backend.AppServices.Azure.Aggregates.Tennant;
using Microsoft.EntityFrameworkCore;

namespace CloudYourself.Backend.AppServices.Azure.Infrastructure
{
    /// <summary>
    /// The azure subscriptions db context containing all aggregates.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class AzureDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AzureDbContext(DbContextOptions<AzureDbContext> options) 
            : base(options) { }

        /// <summary>
        /// Gets or sets the tenants.
        /// </summary>
        /// <value>
        /// The tenants.
        /// </value>
        public DbSet<TenantSettings> TenantSettings { get; set; }

        /// <summary>
        /// Gets or sets the azure subscriptions.
        /// </summary>
        /// <value>
        /// The azure subscriptions.
        /// </value>
        public DbSet<Subscription> Subscriptions { get; set; }

        /// <summary>
        /// Gets or sets the managed resources.
        /// </summary>
        /// <value>
        /// The managed resources.
        /// </value>
        public DbSet<ManagedResource> ManagedResources { get; set; }

        /// <summary>
        /// Gets or sets the managed resource deployments.
        /// </summary>
        /// <value>
        /// The managed resource deployments.
        /// </value>
        public DbSet<ManagedResourceDeployment> ManagedResourceDeployments { get; set; }

        /// <summary>
        /// Called by the context to define th schema.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subscription>().OwnsOne(s => s.Compliance);

            modelBuilder.Entity<TenantSettings>().Property(t => t.Id).ValueGeneratedNever();
            modelBuilder.Entity<TenantSettings>().OwnsOne(t => t.AppRegistration);
            modelBuilder.Entity<TenantSettings>().OwnsOne(t => t.ManagementTarget);

            modelBuilder.Entity<ManagedResource>().OwnsOne(mr => mr.BaseData);
            modelBuilder.Entity<ManagedResource>().OwnsOne(mr => mr.ComplianceSettings);
            modelBuilder.Entity<ManagedResource>().OwnsOne(mr => mr.ArmTemplate);

            modelBuilder.Entity<Subscription>().HasOne<TenantSettings>().WithMany().HasForeignKey(s => s.TenantId);
            modelBuilder.Entity<ManagedResource>().HasOne<TenantSettings>().WithMany().HasForeignKey(mr => mr.TenantId);
            modelBuilder.Entity<ManagedResourceDeployment>().HasOne<ManagedResource>().WithMany().HasForeignKey(mrd => mrd.ManagedResourceId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ManagedResourceDeployment>().HasOne<Subscription>().WithMany().HasForeignKey(mrd => mrd.SubscriptionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
