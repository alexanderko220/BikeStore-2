using BikeStore.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Models.Bikes
{
    public class BikeForCreation
    {
        public long? BikeId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public bool IsInStock { get; set; }
        public decimal? Price { get; set; }
        public string ThumbImgContent { get; set; }
        public long MainCategoryId { get; set; }
        public long SubCategoryId { get; set; }
        public List<Color> ColorList { get; set; }
        public List<Size> SizeList { get; set; }
        
    }
}
