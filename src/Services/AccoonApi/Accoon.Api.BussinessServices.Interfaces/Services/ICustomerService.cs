using Accoon.Api.BussinessServices.Entities.EntityDTOs;
using Accoon.BuildingBlocks.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accoon.Api.BussinessServices.Interfaces.Services
{
    public interface ICustomerService
    {
        List<CustomerDto> GetAllCustomers();
        long SaveCustomer(CustomerDto customer);
        Task<long> SaveCustomerAsync(CustomerDto customer);
    }
}
