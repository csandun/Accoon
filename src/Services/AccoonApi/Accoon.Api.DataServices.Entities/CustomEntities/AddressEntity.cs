using Accoon.BuildingBlocks.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace Accoon.Api.DataServices.Entities.CustomEntities
{
    public class AddressEntity : Entity<int>
    {        
        public int? Number { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }

        public virtual CustomerEntity Customer{ get; set; }
    }
}
