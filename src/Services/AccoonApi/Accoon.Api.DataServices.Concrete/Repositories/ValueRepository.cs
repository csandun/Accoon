using Accoon.Api.DataServices.Entities;
using Accoon.Api.DataServices.Entities.CustomEntities;
using Accoon.Api.DataServices.Interfaces.Repositories;
using Accoon.BuildingBlocks.Common.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accoon.Api.DataServices.Concrete.Repositories
{
    public class ValueRepository : RepositoryBase<AccoonDbContext, AddressEntity, int>, IValueRepository
    {
        public ValueRepository(AccoonDbContext dbContext) : base(dbContext)
        {
        }

        public Task<AddressEntity> TestValueMethod()
        {
            throw new NotImplementedException();
        }
    }
}
