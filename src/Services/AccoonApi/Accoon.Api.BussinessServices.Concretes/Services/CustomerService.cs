using Accoon.Api.BussinessServices.Interfaces.Services;
using Accoon.BuildingBlocks.Common.Interfaces;
using Accoon.Api.DataServices.Interfaces.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.Api.BussinessServices.Concretes.Services
{
    public class CustomerService :  ServiceBase, ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(IMapper mapper, IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
            this._customerRepository = customerRepository;
        }

    }
}
