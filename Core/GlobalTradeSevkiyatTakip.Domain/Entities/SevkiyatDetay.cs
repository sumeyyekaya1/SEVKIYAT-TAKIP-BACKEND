using GlobalTradeSevkiyatTakip.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Domain.Entities
{
    public class SevkiyatDetay : BaseModel
    {
        public int? SevkiyatId { get; set; }
        public Sevkiyat? Sevkiyat { get; set;}
        public int? IrsaliyeId { get; set; }
        public Irsaliye? Irsaliye { get; set; }
        public int? FaturaId { get; set; }
        public Fatura? Fatura { get; set; }

    }
}
