using Accoon.Api.DataServices.Entities.CusotomEntities;
using System.Threading.Tasks;

namespace Accoon.Api.DataServices.Interfaces.Repositories
{
    public interface IValueRepository
    {
        Task<ValueEntity> TestValueMethod();
    }
}
