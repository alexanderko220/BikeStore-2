using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bikeStore.Data.Entities
{
    public class Specification
    {
        [Key]
        public long SpecId { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public long SpecCatId { get; set; }
        public SpecificationCategory SpecCategory { get; set; }
        public IEnumerable<BikesSpecifications> BikesSpecifications { get; set; }

    }
}
