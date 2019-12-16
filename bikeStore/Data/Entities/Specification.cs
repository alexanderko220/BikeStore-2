using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bikeStore.Data.Entities
{
    public class Specification
    {
        [Key]
        public long SpecId { get; set; }
        public string SpecType { get; set; }
        public string SpecBrand { get; set; }
        public string SpecModel { get; set; }
        public string SpecDescr { get; set; }
        public long SpecCatId { get; set; }
        public SpecificationCategory SpecCategory { get; set; }
        public IEnumerable<BikeSpecJunction> BikeSpecJunctions { get; set; }

    }
}
