using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.BuildingBlocks.Common.Pagination
{
    public class PaginationDto<TEntity> where TEntity: class
    {
        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public long Count { get; private set; }

        public IEnumerable<TEntity> Data { get; private set; }
    }
}
