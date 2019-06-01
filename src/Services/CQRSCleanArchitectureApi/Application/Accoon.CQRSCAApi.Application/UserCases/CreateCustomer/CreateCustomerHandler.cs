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

        public CreateCustomerHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<CustomerCreated> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            //insert customer to database
            var newcustomer = new CustomerCreated { CustomerId = Guid.NewGuid() };
            await this.mediator.Publish(newcustomer, cancellationToken);

            return newcustomer;
        }
    }
}
