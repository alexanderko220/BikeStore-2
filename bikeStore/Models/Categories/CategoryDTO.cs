using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Models.Categories
{
    public class CategoryDTO
    {
        public long CatId { get; set; }
        public long? MainCatId { get; set; }
        public string CatName { get; set; }
        public string CatDescr { get; set; }
    }
}
