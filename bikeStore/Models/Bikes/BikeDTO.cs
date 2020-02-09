using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Models.Bikes
{
    public class BikeDTO
    {
        public long BId { get; set; }
        public string BBrand { get; set; }
        public string BModel { get; set; }
        public IEnumerable<IdValue> BSizes { get; set; }
        public IEnumerable<IdValue> BColors { get; set; }
        public bool IsInStock { get; set; }
        public decimal? BPrice { get; set; }
        public byte[] BThumbImgContent { get; set; }
        public long BCategoryId { get; set; }
        public long BImgId { get; set; }
        
    }
}
