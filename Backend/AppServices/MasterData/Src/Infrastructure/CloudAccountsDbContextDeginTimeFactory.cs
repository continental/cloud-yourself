using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace CloudYourself.Backend.AppServices.MasterData.Infrastructure
{
    /// <summary>
    /// A db context factory used to create instances of <see cref="MasterDataDbContext"/> at design time.
    /// </summary>
    public class MasterDataDbContextDeginTimeFactory : IDesignTimeDbContextFactory<MasterDataDbContext>
    {
        /// <summary>
        /// Creates a new instance of a the <see cref="MasterDataDbContext"/>.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>
        /// An instance of a db context.
        /// </returns>
        public MasterDataDbContext CreateDbContext(string[] args)
        {
            Console.Write("Using Design Time Factory!");
            DbContextOptionsBuilder<MasterDataDbContext> optionsBuilder = new DbContextOptionsBuilder<MasterDataDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=CloudYourself.MasterData_DesignTime;User Id=sa;Password=Start@1357;");
            return new MasterDataDbContext(optionsBuilder.Options);
        }
    }
}
    