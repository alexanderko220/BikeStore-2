using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bikeStore.Data.Entities
{
    public class BikeSpecJunction
    {
        [Key]
        public long BSJId { get; set; }

        public long BSJBikeId { get; set; }
        public Bike BSJBike { get; set; }

        public long BSJSpecId { get; set; }
        public Specification BSJSpecification { get; set; }


    }
}
