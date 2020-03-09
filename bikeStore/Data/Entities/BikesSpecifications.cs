using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bikeStore.Data.Entities
{
    public class BikesSpecifications
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long BikeId { get; set; }
        public Bike Bike { get; set; }

        public long SpecificationId { get; set; }
        public Specification Specification { get; set; }


    }
}
