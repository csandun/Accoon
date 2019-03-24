using Accoon.Api.BussinessServices.Interfaces.Services;
using Accoon.Api.DataServices.Entities;
using Accoon.Api.DataServices.Interfaces.Repositories;
using Accoon.BuildingBlocks.Common.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.Api.BussinessServices.Concretes.Services
{
    public class AddressService: ServiceBase, IAddressService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<AccoonDbContext> _unitOfWork;
        private readonly IAddressRepository _addressRepository;

        public AddressService(IMapper mapper, IUnitOfWork<AccoonDbContext> unitOfWork, IAddressRepository addressRepository)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
            this._addressRepository = addressRepository;
        }
    }
}
