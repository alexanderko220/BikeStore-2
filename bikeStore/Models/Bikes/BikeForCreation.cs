using BikeStore.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Models.Bikes
{
    public class BikeForCreation
    {
        [FromForm(Name = "bikeId")]
        public long? BikeId { get; set; }

        [FromForm(Name = "brand")]
        public string Brand { get; set; }

        [FromForm(Name = "model")]
        public string Model { get; set; }

        [FromForm(Name = "isInStock")]
        public bool IsInStock { get; set; }

        [FromForm(Name = "price")]
        public decimal? Price { get; set; }

        [FromForm(Name = "thumbBase64")]
        public string ThumbBase64 { get; set; }

        [FromForm(Name = "categoryId")]
        public long CategoryId { get; set; }

        [FromForm(Name = "mainCategoryId")]
        public long? MainCategoryId { get; set; }

        [FromForm(Name = "imgId")]
        public long? ImgId { get; set; }

        [FromForm(Name = "sizes")]
        public IEnumerable<long> Sizes { get; set; }

        [FromForm(Name = "colors")]
        public IEnumerable<long> Colors { get; set; }

        [FromForm(Name = "thumbFileName")]
        public string ThumbFileName { get; set; }

        [FromForm(Name = "junkColors")]
        public IEnumerable<BikesColorsDto> JunkColors { get; set; }
        [FromForm(Name = "junkSizes")]
        public IEnumerable<BikesSizesDto> JunkSizes { get; set; }
    }
}
