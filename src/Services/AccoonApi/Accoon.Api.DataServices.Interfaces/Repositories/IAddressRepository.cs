using Accoon.Api.DataServices.Entities;
using Accoon.Api.DataServices.Entities.CustomEntities;
using Accoon.BuildingBlocks.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.Api.DataServices.Interfaces.Repositories
{
    public interface IAddressRepository : IRepository<AccoonDbContext, AddressEntity, int>
    {
    }
}
