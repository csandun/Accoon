using Accoon.BuildingBlocks.Common.Entities;

namespace Accoon.Api.BussinessServices.Entities.CustomEntities
{
    public class ValueModal: Entity<int>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}
