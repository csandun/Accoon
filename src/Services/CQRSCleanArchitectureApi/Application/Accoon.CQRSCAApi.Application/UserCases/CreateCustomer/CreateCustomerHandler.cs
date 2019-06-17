using Accoon.CQRSCAApi.Application.Interfaces;
using Accoon.CQRSCLApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accoon.CQRSCAApi.Application.UserCases.CreateCustomer
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CustomerCreated>
    {
        private readonly IMediator mediator;
        private readonly ICqrscaDbContext cqrscaDbContext;

        public CreateCustomerHandler(IMediator mediator, ICqrscaDbContext context)
        {
            this.mediator = mediator;
            this.cqrscaDbContext = context;
        }

        public async Task<CustomerCreated> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Customer()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Age = request.Age
            };
            //insert customer to database
            this.cqrscaDbContext.Customers.Add(entity);

            //await this.cqrscaDbContext.SaveChangesAsync(cancellationToken);

            var newcustomer = new CustomerCreated { CustomerId = entity.Id };
            await this.mediator.Publish(newcustomer, cancellationToken);

            return newcustomer;
        }
    }
}
