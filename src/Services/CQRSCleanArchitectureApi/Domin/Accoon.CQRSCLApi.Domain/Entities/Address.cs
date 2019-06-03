using Accoon.BuildingBlocks.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.CQRSCLApi.Domain.Entities
{
    public class Address : Entity<Guid>
    {
        public int? Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }

        //public virtual Customer Customer { get; set; }
    }
}
