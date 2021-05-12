using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace CloudYourself.Backend.AppServices.CloudAccounts.Infrastructure
{
    /// <summary>
    /// A db context factory used to create instances of <see cref="CloudAccountsDbContext"/> at design time.
    /// </summary>
    public class CloudAccountsDbContextDeginTimeFactory : IDesignTimeDbContextFactory<CloudAccountsDbContext>
    {
        /// <summary>
        /// Creates a new instance of a the <see cref="CloudAccountsDbContext"/>.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>
        /// An instance of a db context.
        /// </returns>
        public CloudAccountsDbContext CreateDbContext(string[] args)
        {
            Console.Write("Using Design Time Factory!");
            DbContextOptionsBuilder<CloudAccountsDbContext> optionsBuilder = new DbContextOptionsBuilder<CloudAccountsDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=CloudYourself.CloudAccounts_DesignTime;User Id=sa;Password=Start@1357;");
            return new CloudAccountsDbContext(optionsBuilder.Options);
        }
    }
}
    