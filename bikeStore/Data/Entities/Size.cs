using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Data.Entities
{
    public class Size
    {
        public long? SizeId { get; set; }
        public string SizeValue { get; set; }
        public string SizeName { get; set; }
        public IEnumerable<BikesColors> BikesColorSize { get; set; }
    }
}
