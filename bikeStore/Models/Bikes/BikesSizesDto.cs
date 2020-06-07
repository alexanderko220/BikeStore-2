using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Models.Bikes
{
    public class BikesSizesDto
    {
        [FromForm(Name = "id")]
        public long Id { get; set; }
        [FromForm(Name = "bikeId")]
        public long BikeId { get; set; }
        [FromForm(Name = "sizeId")]
        public long SizeId { get; set; }
    }
}
