using Accoon.BuildingBlocks.EventBus.Abstractions;
using AccoonTest.Api.IntergrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccoonTest.Api.IntergrationEvents.EventHandling
{
    public class CustomerCreatedIntergrationEventHandler : IIntegrationEventHandler<CustomerCreatedIntegrationEvent>
    {
        public Task Handle(CustomerCreatedIntegrationEvent @event)
        {
            var evnet = @event;
            Console.WriteLine(@event.CustomerId);
            return Task.CompletedTask;

        }
    }
}
