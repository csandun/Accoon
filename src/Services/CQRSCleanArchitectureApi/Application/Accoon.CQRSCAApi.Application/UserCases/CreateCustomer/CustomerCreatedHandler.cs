using Accoon.BuildingBlocks.EventBus.Abstractions;
using Accoon.CQRSCAApi.Application.Interfaces;
using Accoon.CQRSCAApi.Application.Notification.Model;
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
        private readonly IEventBus eventBus;
        public CustomerCreatedHandler(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }
        public Task Handle(CustomerCreated response, CancellationToken cancellationToken)
        {
            var customerCreatedEvent = new CustomerCreatedIntegrationEvent(response.CustomerId);

            this.eventBus.Publish(customerCreatedEvent);

            return Task.FromResult(response);

        }
    }
}
