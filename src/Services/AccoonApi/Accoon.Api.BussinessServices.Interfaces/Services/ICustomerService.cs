using Accoon.Api.BussinessServices.Entities.EntityDTOs;
using Accoon.BuildingBlocks.Common.Interfaces;
using Accoon.BuildingBlocks.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accoon.Api.BussinessServices.Interfaces.Services
{
    public interface ICustomerService
    {
        long SaveCustomer(CustomerDto customer);
        Task<long> SaveCustomerAsync(CustomerDto customer);
        Task<CustomerDto> GetCustomerByIdAsync(long id);
        PaginationDto<CustomerDto> GetCustomers(int page = 1, int size = 10);
    }
}
