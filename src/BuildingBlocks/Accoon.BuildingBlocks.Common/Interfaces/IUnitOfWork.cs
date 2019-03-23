using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.BuildingBlocks.Common.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        int Commit();
    }
}
