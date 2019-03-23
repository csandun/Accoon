using Accoon.BuildingBlocks.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.Api.DataServices.Entities.CustomEntities
{
    public class CustomerEntity: Entity<long>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public  ICollection<AddressEntity> Addresses { get; set; }
    }
}
