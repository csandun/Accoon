using Accoon.CQRSCAApi.Application.Interfaces;
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

        public GetCustomersListQueryHandler(ICqrscaDbContext cqrscaDbContext)
        {
            this.cqrscaDbContext = cqrscaDbContext;
        }

        public async Task<CustomerListViewModel> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
        {
            // get from database
            var list = await this.cqrscaDbContext.Customers.Select(o => new CustomerDetailModel {
                   Id = o.Id,
                   Name = o.Name,
                   Age = o.Age
            }).ToListAsync();

            var model =  new CustomerListViewModel()
            {
                Customers = list
            };

            return model;
        }
    }
}
