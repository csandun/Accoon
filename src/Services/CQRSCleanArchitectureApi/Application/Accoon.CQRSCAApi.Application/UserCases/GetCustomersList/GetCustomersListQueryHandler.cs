using Accoon.CQRSCAApi.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accoon.CQRSCAApi.Application.UserCases.GetCustomersList
{
    public class GetCustomersListQueryHandler : IRequestHandler<GetCustomersListQuery, CustomerListViewModel>
    {
        private readonly ICqrscaDbContext cqrscaDbContext;
        private readonly IMapper mapper;

        public GetCustomersListQueryHandler(ICqrscaDbContext cqrscaDbContext, IMapper mapper)
        {
            this.cqrscaDbContext = cqrscaDbContext;
            this.mapper = mapper;
        }

        public async Task<CustomerListViewModel> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
        {
            // get from database
            var customerList = await this.cqrscaDbContext.Customers
                .ProjectTo<CustomerDetailModel>(this.mapper.ConfigurationProvider).ToListAsync<CustomerDetailModel>(cancellationToken);

            var model =  new CustomerListViewModel()
            {
                Customers = customerList
            };

            return model;
        }
    }
}
