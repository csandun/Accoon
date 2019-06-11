using Accoon.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.CQRSCAApi.Application.UserCases.CreateCustomer
{
    public class CustomerCreatedIntegrationEvent: IntegrationEvent
    {
        public Guid CustomerId { get; set; }

        public CustomerCreatedIntegrationEvent(Guid customerId)
        {
            this.CustomerId = customerId;

        }
    }
}
