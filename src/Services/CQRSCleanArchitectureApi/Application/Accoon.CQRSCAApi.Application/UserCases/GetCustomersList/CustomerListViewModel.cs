using System.Collections.Generic;

namespace Accoon.CQRSCAApi.Application.UserCases.GetCustomersList
{
    public class CustomerListViewModel
    {
        public List<CustomerDetailModel> Customers { get; set; }
    }
}