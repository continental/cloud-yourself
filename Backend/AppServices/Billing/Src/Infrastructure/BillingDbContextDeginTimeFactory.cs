using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace CloudYourself.Backend.AppServices.Billing.Infrastructure
{
    /// <summary>
    /// A db context factory used to create instances of <see cref="BillingDbContext"/> at design time.
    /// </summary>
    public class BillingDbContextDeginTimeFactory : IDesignTimeDbContextFactory<BillingDbContext>
    {
        /// <summary>
        /// Creates a new instance of a the <see cref="BillingDbContext"/>.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>
        /// An instance of a db context.
        /// </returns>
        public BillingDbContext CreateDbContext(string[] args)
        {
            Console.Write("Using Design Time Factory!");
            DbContextOptionsBuilder<BillingDbContext> optionsBuilder = new DbContextOptionsBuilder<BillingDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=CloudYourself.Billing_DesignTime;User Id=sa;Password=Start@1357;");
            return new BillingDbContext(optionsBuilder.Options);
        }
    }
}
    