using System;
using System.ComponentModel.DataAnnotations;

namespace bikeStore.Data.Entities
{
    public class ImgContent
    {
        [Key]
        public long ImgContentId { get; set; }

        public string ImgContentName { get; set; }

        public string  ImgContentMimeType { get; set; }

        public bool IsThumbnail { get; set; }
        public DateTime? ImgCreateDt { get; set; }

        public byte[] Content { get; set; }

        public long StoreImgId { get; set; }
        public StoreImages StoreImages { get; set; }
    }
}
