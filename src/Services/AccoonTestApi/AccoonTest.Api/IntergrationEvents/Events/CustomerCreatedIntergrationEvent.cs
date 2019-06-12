using Accoon.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccoonTest.Api.IntergrationEvents.Events
{
    public class CustomerCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid CustomerId { get; set; }

        public CustomerCreatedIntegrationEvent(Guid customerId)
        {
            this.CustomerId = customerId;

        }
    }
}
