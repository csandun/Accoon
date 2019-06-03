using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Accoon.CQRSCLApi.Persistence
{
  

    public class CQRSCADbContextFactory : IDesignTimeDbContextFactory<CqrscaDbContext>
    {
        private readonly DbContextOptions<CqrscaDbContext> options;

        public CQRSCADbContextFactory()
        {

        }

        public CQRSCADbContextFactory(DbContextOptions<CqrscaDbContext> options)
        {
            this.options = options;
        }
        public CqrscaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CqrscaDbContext>();
            optionsBuilder.UseSqlServer("Server=tcp:127.0.0.1,1433;Database=AccoonCQRSDatabase;User Id=sa;Password=Ringer#123;ConnectRetryCount=0;");
                

            return new CqrscaDbContext(optionsBuilder.Options);
        }
    }

    // https://blog.tonysneed.com/2018/12/20/idesigntimedbcontextfactory-and-dependency-injection-a-love-story/
    // https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation
}
