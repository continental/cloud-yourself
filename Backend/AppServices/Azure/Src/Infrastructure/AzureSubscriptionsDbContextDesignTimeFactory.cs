using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace CloudYourself.Backend.AppServices.Azure.Infrastructure
{
    /// <summary>
    /// A db context factory used to create instances of <see cref="CloudAccountsDbContext"/> at design time.
    /// </summary>
    public class AzureDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AzureDbContext>
    {
        /// <summary>
        /// Creates a new instance of a the <see cref="CloudAccountsDbContext"/>.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>
        /// An instance of a db context.
        /// </returns>
        public AzureDbContext CreateDbContext(string[] args)
        {
            Console.Write("Using Design Time Factory!");
            DbContextOptionsBuilder<AzureDbContext> optionsBuilder = new DbContextOptionsBuilder<AzureDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=CloudYourself.CloudAccounts_DesignTime;User Id=sa;Password=Start@1357;");
            return new AzureDbContext(optionsBuilder.Options);
        }
    }
}
    