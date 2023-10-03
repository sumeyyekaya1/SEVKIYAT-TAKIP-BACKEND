using GlobalTradeSevkiyatTakip.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Domain.Entities
{
    public class SevkiyatNot:BaseModel
    {
        public string? Metin { get;set; }
        public int? KullaniciId { get;set; }
        public Kullanici? Kullanici { get;set; }
    }
}
