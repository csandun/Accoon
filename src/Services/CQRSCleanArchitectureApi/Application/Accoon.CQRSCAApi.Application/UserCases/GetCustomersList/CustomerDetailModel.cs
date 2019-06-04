using System;

namespace Accoon.CQRSCAApi.Application.UserCases.GetCustomersList
{
    public class CustomerDetailModel
    {
        public Guid Id{ get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}