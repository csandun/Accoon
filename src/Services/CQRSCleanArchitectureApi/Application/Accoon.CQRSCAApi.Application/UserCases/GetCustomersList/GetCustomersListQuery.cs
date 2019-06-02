using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.CQRSCAApi.Application.UserCases.GetCustomersList
{
    public class GetCustomersListQuery: IRequest<CustomerListViewModel>
    {
    }
}
