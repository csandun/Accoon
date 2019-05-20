using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accoon.CQRSCAApi.Application.UserCases.CreateCustomer
{
    public class CustomerCreatedHandler : INotificationHandler<CustomerCreated>
    {
        public Task Handle(CustomerCreated notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
