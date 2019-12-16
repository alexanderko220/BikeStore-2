using BikeStore.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bikeStore.Data.Entities
{
   
    public class Bike
    {
        [Key]
        public long BId { get; set; }
        public string BBrand { get; set; }
        public string BModel { get; set; }
        //public string BSize { get; set; }
        //public string BColor { get; set; }
        public bool IsInStock { get; set; }
        public decimal? BPrice { get; set; }
        public byte[] BThumbImgContent { get; set; }
        public long BCategoryId { get; set; }
        public Category BCategory { get; set; }
        public IEnumerable<BikeSpecJunction> BikeSpecJunctions { get; set; }
        public IEnumerable<BikeJunction> BikeJunctions { get; set; }

        public long BImgId { get; set; }
        public StoreImages BImages { get; set; } 
    }
}
