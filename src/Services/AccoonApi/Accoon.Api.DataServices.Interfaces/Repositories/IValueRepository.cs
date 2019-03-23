using Accoon.Api.DataServices.Entities.CustomEntities;
using Accoon.BuildingBlocks.Common.Interfaces;
using System.Threading.Tasks;

namespace Accoon.Api.DataServices.Interfaces.Repositories
{
    public interface IValueRepository: IRepository<AddressEntity, int>
    {
        Task<AddressEntity> TestValueMethod();
    }
}
