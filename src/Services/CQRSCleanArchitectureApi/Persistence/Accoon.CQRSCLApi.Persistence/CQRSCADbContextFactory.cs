using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.CQRSCLApi.Persistence
{
  

    public class CQRSCADbContextFactory : IDesignTimeDbContextFactory<CqrscaDbContext>
    {
        public CqrscaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CqrscaDbContext>();
            //optionsBuilder.UseSqlServer("Data Source=blog.db");

            return new CqrscaDbContext(optionsBuilder.Options);
        }
    }
}
