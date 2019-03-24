using Accoon.Api.BussinessServices.Interfaces.Services;
using Accoon.BuildingBlocks.Common.Interfaces;
using Accoon.Api.DataServices.Interfaces.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Accoon.Api.DataServices.Entities;
using Accoon.Api.BussinessServices.Entities.EntityDTOs;
using System.Linq;

namespace Accoon.Api.BussinessServices.Concretes.Services
{
    public class CustomerService :  ServiceBase, ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<AccoonDbContext> _unitOfWork;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(IMapper mapper, IUnitOfWork<AccoonDbContext> unitOfWork, ICustomerRepository customerRepository)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
            this._customerRepository = customerRepository;
        }

        public List<CustomerDto> GetAllCustomers()
        {
            var customers = this._customerRepository.GetAll().ToList();
            var result = this._mapper.Map<List<CustomerDto>>(customers);
            return result;
        }

    }
}
