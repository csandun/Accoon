using Accoon.BuildingBlocks.Common.Entities;

namespace Accoon.Api.DataServices.Entities.CustomEntities
{
    public class ValueEntity : Entity<int>
    {
        public string Name { get; set; }
        public int Age{ get; set; }
        public string Address { get; set; }
    }
}
