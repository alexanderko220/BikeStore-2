using System.ComponentModel.DataAnnotations;

namespace bikeStore.Data.Entities
{
    public class ImgContent
    {
        [Key]
        public long ICId { get; set; }

        public string ICName { get; set; }

        public string  ICMimeType { get; set; }

        public byte[] ICContent { get; set; }
    }
}
