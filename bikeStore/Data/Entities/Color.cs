using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Data.Entities
{
    public class Color
    {
        public long ColorId { get; set; }
        public string ColorValue { get; set; }
        public string ColorName { get; set; }
        public IEnumerable<BikesColorSize> BikesColorSize { get; set; }
    }
}
