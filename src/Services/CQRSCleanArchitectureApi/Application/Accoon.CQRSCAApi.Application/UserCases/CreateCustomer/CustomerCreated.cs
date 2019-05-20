using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.CQRSCAApi.Application.UserCases.CreateCustomer
{
    public class CustomerCreated: INotification
    {
        public Guid CustomerId { get; set; }
    }
}
