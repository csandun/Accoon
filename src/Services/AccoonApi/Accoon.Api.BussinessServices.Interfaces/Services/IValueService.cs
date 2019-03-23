using Accoon.Api.BussinessServices.Entities.EntityDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accoon.Api.BussinessServices.Interfaces.Services
{
    public interface IValueService
    {
        Task<ValueDTO> TestValueServiceMethod();
    }
}
