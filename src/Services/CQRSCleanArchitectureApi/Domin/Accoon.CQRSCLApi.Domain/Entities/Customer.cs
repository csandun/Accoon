using Accoon.BuildingBlocks.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.CQRSCLApi.Domain.Entities
{
    public class Customer: Entity<Guid>
    {
        public Customer()
        {
            Addresses = new HashSet<Address>();
        }

        public string Name { get; set; }
        public int Age { get; set; }

        public ICollection<Address> Addresses { get; private set ; } // cannot assign the value
    }
}
