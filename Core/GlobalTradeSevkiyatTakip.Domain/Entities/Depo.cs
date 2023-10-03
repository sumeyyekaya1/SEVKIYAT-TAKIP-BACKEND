using GlobalTradeSevkiyatTakip.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Domain.Entities
{
    public class Depo : BaseModel
    {
        public Int64? WolvoxBlKodu { get; set; }
        public string? DepoAd { get; set; }
        public string? DepoAdres { get; set; }
        public string? DepoIletisim { get; set; }
        public string? DepoYetkili { get; set; }
        public ICollection<DepoDetay>? Detay { get; set; }
    }
}
