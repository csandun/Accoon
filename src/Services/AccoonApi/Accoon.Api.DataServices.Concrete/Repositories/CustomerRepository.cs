using Accoon.Api.DataServices.Entities.CustomEntities;
using Accoon.Api.DataServices.Interfaces.Repositories;
using Accoon.BuildingBlocks.Common.Concretes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.Api.DataServices.Concrete.Repositories
{
    public class CustomerRepository: RepositoryBase<CustomerEntity, long>, ICustomerRepository
    {
    }
}
