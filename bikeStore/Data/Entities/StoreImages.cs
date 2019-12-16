using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bikeStore.Data.Entities
{
    public class StoreImages
    {
        [Key]
        public long SIId { get; set; }
        
        public string SIDescription { get; set; }

        public IEnumerable<ImgContent> SIImeges { get; set; }  
    }
}
