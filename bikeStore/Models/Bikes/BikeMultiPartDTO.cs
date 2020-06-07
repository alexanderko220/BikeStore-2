using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Data.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Models.Bikes
{
    public class BikeMultiPartDTO
    {
        [FromForm(Name = "files[]")]
        public IList<IFormFile> Files { get; set; }
        [FromJson]
        [FromForm(Name = "bike")]
        public BikeForCreation Bike { get; set; } // <-- JSON will be deserialized to this object
    }
}
