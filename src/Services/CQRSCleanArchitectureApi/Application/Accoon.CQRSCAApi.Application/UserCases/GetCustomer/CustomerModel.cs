using Accoon.CQRSCAApi.Application.Interfaces.Mapping;
using Accoon.CQRSCLApi.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.CQRSCAApi.Application.UserCases.GetCustomer
{
    public class CustomerModel: IMapFrom<Customer>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
