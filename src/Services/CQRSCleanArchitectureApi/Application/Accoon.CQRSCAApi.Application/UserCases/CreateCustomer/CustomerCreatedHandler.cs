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
        private readonly INotificationService notificationService;
        public CustomerCreatedHandler(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        public async Task Handle(CustomerCreated notification, CancellationToken cancellationToken)
        {
            await this.notificationService.SendAsync(new Message() { Body = notification.CustomerId.ToString()});
        }
    }
}
