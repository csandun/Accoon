using Accoon.CQRSCAApi.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
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

        public Task<CustomerListViewModel> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
        {
            // get from database

            

            var customerList = new List<CustomerDetailModel>() {
                new CustomerDetailModel(){ Id = Guid.NewGuid(), Name="Sandun" },
                new CustomerDetailModel(){ Id = Guid.NewGuid(), Name="Kumara" }

            };

            var model =  new CustomerListViewModel()
            {
                Customers = customerList
            };

            return Task.FromResult(model);
        }
    }
}
