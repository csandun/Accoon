using Accoon.CQRSCLApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.CQRSCAApi.Application.Interfaces
{
    public interface ICqrscaDbContext
    {
        DbSet<Customer> Customers { get; set; }
    }
}
