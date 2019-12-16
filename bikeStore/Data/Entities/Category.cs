using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bikeStore.Data.Entities
{
    public class Category
    {
        [Key]
        public long CatId { get; set; }
        public long? MainCatId { get; set; }
        public string CatName { get; set; }
        public bool IsCategoryActive { get; set; }
        public string CatDescr { get; set; }
        public IEnumerable<Bike> Bikes { get; set; }
    }
}
