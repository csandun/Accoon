using Accoon.Api.DataServices.Entities.CustomEntities;
using System.Threading.Tasks;

namespace Accoon.Api.DataServices.Interfaces.Repositories
{
    public interface IValueRepository
    {
        Task<ValueEntity> TestValueMethod();
    }
}
