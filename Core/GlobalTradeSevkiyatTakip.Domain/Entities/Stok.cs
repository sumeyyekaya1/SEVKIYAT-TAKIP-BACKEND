using GlobalTradeSevkiyatTakip.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Domain.Entities
{
    public class Stok : BaseModel
    {
        public Int64? WolvoxBlKodu { get; set; }
        public string? StokKod { get; set; }
        public string? Barkod { get; set; }
        public string? StokAdi { get; set; }
        public String? KdvOran { get; set; }
        public int? RenkId { get; set; }
        public Renk? Renk { get; set; }
        public int? MarkaId { get; set; }
        public Marka? Marka { get; set; }
        public int? DepoId { get; set; }
        public Depo? Depo { get; set; }
        public string? IskontoOran { get; set; }
        public decimal? Agirlik { get; set; }
        public decimal? DesiMiktari { get; set; }
        public string? DovizBirim { get; set; }
       
      
    }
}
