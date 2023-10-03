using GlobalTradeSevkiyatTakip.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Domain.Entities
{
    public class IrsaliyeDetay : BaseModel
    {
        public Int64? WolvoxBlKodu { get; set; }
        public int? IrsaliyeId { get; set; }
        public Irsaliye? Irsaliye { get; set; }
        public int? StokId { get; set; }
        public Stok? Stok { get; set; }
        public int? HizmetId { get; set; }
        public Hizmet? Hizmet { get; set; }
        public int? DepoId { get; set; }
        public Depo? Depo { get; set; }
        public int? Miktar { get; set; }
        public bool? TakimDurum { get; set; }
        public string? Icindekiler { get; set; }
        public string? Olculer { get; set; }
        public string? KapNo { get; set; }
        public string? PaketSekil { get; set; }
        public int? PaketIciAdet { get; set; }
        public int? ToplamPaketIciAdet { get; set; }
        public int? KapAdet { get; set; }
        public int? ToplamKapAdet { get; set; }
        public string? TedarikFirma { get; set; }
        public string? UrunIcerik { get; set; }
        public decimal? UrunBurutAgirlik { get; set; }
        public decimal? UrunNetAgirlik { get; set; }
    }
}
