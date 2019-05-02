using Accoon.BuildingBlocks.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.BuildingBlocks.Common.Concretes
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext dbContext;
        public UnitOfWork(TDbContext context)
        {
            this.dbContext = context;
        }

        public int Commit()
        {
            this.dbContext.SaveChanges();
            return 1;
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
        }
    }
}
