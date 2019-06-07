using Accoon.CQRSCAApi.Application.Interfaces;
using Accoon.CQRSCLApi.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accoon.CQRSCAApi.Application.UserCases.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerModel>
    {
        private readonly ICqrscaDbContext cqrscaDbContext;
        private readonly IMapper mapper;
        
        public GetCustomerQueryHandler(ICqrscaDbContext cqrscaDbContext, IMapper mapper)
        {
            this.cqrscaDbContext = cqrscaDbContext;
            this.mapper = mapper;
        }

        public async Task<CustomerModel> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer =  await this.cqrscaDbContext.Customers.FirstAsync(o => o.Id.Equals(request.Id));
            var getCustomer  = this.mapper.Map<CustomerModel>(customer);
            return getCustomer;
        }
    }
}
