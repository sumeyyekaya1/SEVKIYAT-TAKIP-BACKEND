using GlobalTradeSevkiyatTakip.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Domain.Entities
{
    public class FaturaDetay : BaseModel
    {
        public Int64? WolvoxBlKodu { get; set; }
        public int? FaturaId { get; set; }
        public Fatura? Fatura { get; set; }
        public int? StokId { get; set; }
        public Stok? Stok { get; set; }
        public int? DovizId { get; set; }
        public Doviz? Doviz { get; set; }
        public int? HizmetId { get; set; }
        public Hizmet? Hizmet { get; set; }
        public decimal? Miktar { get; set; }
        public decimal? ToplamTutar { get; set; }

    }
}
