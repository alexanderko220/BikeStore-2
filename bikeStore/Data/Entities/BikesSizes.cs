using bikeStore.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeStore.Data.Entities
{   
    /// <summary>
    /// Many To Many relation between Bikes and Sizes
    /// </summary>
    public class BikesSizes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long BikeId { get; set; }
        public long SizeId { get; set; }
        public Bike Bike { get; set; }
        public Size Size { get; set; }
    }
}
