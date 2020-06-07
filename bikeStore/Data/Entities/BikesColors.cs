using System;
using bikeStore.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeStore.Data.Entities
{
    /// <summary>
    /// Many To Many relation between Bikes and Colors
    /// </summary>
    public class BikesColors
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       
        public long Id { get; set; }
        public long BikeId { get; set; }
        public long ColorId { get; set; }
        public Bike Bike { get; set; }
        public Color Color { get; set; }
       
    }
}
