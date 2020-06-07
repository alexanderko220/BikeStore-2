using BikeStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Models.Bikes
{
    public class BikeDTO
    {
        public long BikeId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public IEnumerable<long> Sizes { get; set; }
        public IEnumerable<long> Colors { get; set; }
        public bool IsInStock { get; set; }
        public decimal Price { get; set; }
        public string ThumbBase64 { get; set; }
        public long CategoryId { get; set; }
        public long? MainCategoryId { get; set; }
        public string CategoryName { get; set; }
        public long? ImgId { get; set; }
        public IEnumerable<BikesColorsDto> JunkColors { get; set; }
        public IEnumerable<BikesSizesDto> JunkSizes { get; set; }
    }
}
