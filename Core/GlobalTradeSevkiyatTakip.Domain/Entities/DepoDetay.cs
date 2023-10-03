using GlobalTradeSevkiyatTakip.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Domain.Entities
{
    public class DepoDetay : BaseModel
    {
        public Int64? WolvoxBlKodu { get; set; }
        public int? DepoId { get; set; }
        public Depo? Depo { get; set; }
        public int? StokId { get; set; }
        public Stok? Stok { get; set; }
        public int? CariId { get; set; }
        public Cari? Cari { get; set; }
        public int? Adet { get; set; }
        public DateTime? TarihSaat { get; set; }
        public string? Aciklama { get; set; }
    }
}
