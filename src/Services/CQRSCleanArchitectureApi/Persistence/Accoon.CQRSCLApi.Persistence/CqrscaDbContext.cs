using Accoon.CQRSCAApi.Application.Interfaces;
using Accoon.CQRSCLApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.CQRSCLApi.Persistence
{
    public class CqrscaDbContext : DbContext, ICqrscaDbContext
    {
        public CqrscaDbContext(DbContextOptions<CqrscaDbContext> options):base(options)
        {
        }
        public DbSet<Customer> Customers { get ; set ; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CqrscaDbContext).Assembly);
        }
    }
}
