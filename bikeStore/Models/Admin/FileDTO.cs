using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Models.Admin
{
    public class FileDTO
    {
        public long FileId { get; set; }

        public string FileName { get; set; }

        public string FileType { get; set; }

        public DateTime? FileCreateDt { get; set; }

        public string Base64String { get; set; }

        public bool IsThumbnail { get; set; }
    }
}
