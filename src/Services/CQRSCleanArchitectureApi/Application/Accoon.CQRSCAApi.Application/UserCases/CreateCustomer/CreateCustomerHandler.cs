using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accoon.CQRSCAApi.Application.UserCases.CreateCustomer
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Unit>
    {
        private readonly IMediator mediator;

        public CreateCustomerHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            await this.mediator.Publish(new CustomerCreated { CustomerId = Guid.NewGuid() }, cancellationToken);

            return Unit.Value;
        }
    }
}
