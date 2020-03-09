using bikeStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Data.Entities
{
    public class BikesColorSize
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        public long BikeId { get; set; }
        public long ColorId { get; set; }
        public long SizeId { get; set; }

        public Bike Bike { get; set; }
        public Color Color { get; set; }

        public Size Size { get; set; }

    }
}
