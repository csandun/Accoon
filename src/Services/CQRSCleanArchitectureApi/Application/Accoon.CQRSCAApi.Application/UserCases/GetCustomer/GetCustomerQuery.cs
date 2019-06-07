using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.CQRSCAApi.Application.UserCases.GetCustomer
{
    public class GetCustomerQuery: IRequest<CustomerModel>
    {
        public Guid Id { get; set; }
    }
}
