using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace CloudYourself.Backend.AppServices.Aws.Infrastructure
{
    /// <summary>
    /// A db context factory used to create instances of <see cref="CloudAccountsDbContext"/> at design time.
    /// </summary>
    public class AwsDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AwsDbContext>
    {
        /// <summary>
        /// Creates a new instance of a the <see cref="AwsDbContext"/>.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>
        /// An instance of a db context.
        /// </returns>
        public AwsDbContext CreateDbContext(string[] args)
        {
            Console.Write("Using Design Time Factory!");
            DbContextOptionsBuilder<AwsDbContext> optionsBuilder = new DbContextOptionsBuilder<AwsDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=CloudYourself.CloudAccounts_DesignTime;User Id=sa;Password=Start@1357;");
            return new AwsDbContext(optionsBuilder.Options);
        }
    }
}
    