using BikeStore.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bikeStore.Data.Entities
{
   
    public class Bike
    {
        [Key]
        public long BikeId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public bool IsInStock { get; set; }
        public decimal? Price { get; set; }
        public byte[] ThumbImgContent { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public long ImgId { get; set; }
        public StoreImages Images { get; set; }
        public IEnumerable<BikesSpecifications> Specifications { get; set; }
        public IEnumerable<BikesColorSize> ColorSize { get; set; }
    }
}
