using Accoon.Api.DataServices.Entities.CusotomEntities;
using Accoon.Api.DataServices.Interfaces.Repositories;
using Accoon.BuildingBlocks.Common.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accoon.Api.DataServices.Concrete.Repositories
{
    public class ValueRepository : RepositoryBase<ValueEntity, int>, IValueRepository
    {
        public override void Delete(ValueEntity entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<ValueEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public override ValueEntity Insert(ValueEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<ValueEntity> TestValueMethod()
        {
            throw new NotImplementedException();
        }

        public override ValueEntity Update(ValueEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
