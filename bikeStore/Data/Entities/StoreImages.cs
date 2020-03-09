using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bikeStore.Data.Entities
{
    public class StoreImages
    {
        [Key]
        public long StoreImgId { get; set; }
        
        public string Description { get; set; }

        public IEnumerable<ImgContent> ImgContents { get; set; }  
    }
}
