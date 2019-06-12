using Accoon.CQRSCLApi.Persistence.infastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Diagnostics;
using System.IO;

namespace Accoon.CQRSCLApi.Persistence
{
  

    public class CQRSCADbContextFactory : IDesignTimeDbContextFactory<CqrscaDbContext>
    {
        public CQRSCADbContextFactory()
        {
            Debugger.Launch();
        }

        public CqrscaDbContext CreateDbContext(string[] args)
        {
            var currentDirentory = Path.Combine(Directory.GetCurrentDirectory());
            Console.WriteLine(currentDirentory);
            var resolver = new DependencyResolver
            {
                CurrentDirectory = currentDirentory
               
            };

            return resolver.ServiceProvider.GetService(typeof(CqrscaDbContext)) as CqrscaDbContext;
        }
    }
}

// Design-time DbContext Creation in code first migration
// https://blog.tonysneed.com/2018/12/20/idesigntimedbcontextfactory-and-dependency-injection-a-love-story/
// https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation

// Debug the senario
// Debugger.Launch()
// Add Debugger.Launch() to the beginning of the constructor to launch the just-in-time debugger. This lets you attach VS to the process and debug it like normal.
