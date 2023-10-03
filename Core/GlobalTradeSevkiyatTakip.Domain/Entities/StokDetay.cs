using GlobalTradeSevkiyatTakip.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Domain.Entities
{
    public class StokDetay : BaseModel
    {
        public Int64? WolvoxBlKodu { get; set; }
        public int? DepoId { get; set; }
        public Depo? Depo { get; set; }
        public int? CariId { get; set; }
        public Cari? Cari { get; set; }
        public int? StokId { get; set; }
        public Stok? Stok { get; set; }
        public decimal? GirisMiktar { get; set; }
        public decimal? CikisMiktar { get; set; }
        public decimal? NetMiktar { get; set; }
    }
}
