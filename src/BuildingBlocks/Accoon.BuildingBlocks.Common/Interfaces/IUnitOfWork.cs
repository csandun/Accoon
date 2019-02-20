using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Accoon.BuildingBlocks.Common.Interfaces
{
    public interface IUnitOfWork<TDbContext>: IDisposable where TDbContext: DbContext
    {
        Task SaveChanges();
    }
}
