using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bikeStore.Data.Entities
{
    public class SpecificationCategory
    {
        [Key]
        public long SpecCatId { get; set; }
        public string SpecCatName { get; set; }
        public bool IsSpecCatActive { get; set; }
        public IEnumerable<Specification> Specifications { get; set; }
    }
}
