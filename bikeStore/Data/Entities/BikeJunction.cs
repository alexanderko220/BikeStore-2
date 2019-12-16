using bikeStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Data.Entities
{
    public class BikeJunction
    {
        [Key]
        public long BJId { get; set; }

        public long BId { get; set; }
        public Bike BJBike { get; set; }
        public Color BJColor { get; set; }
        public Size BJSize { get; set; }
            
    }
}
