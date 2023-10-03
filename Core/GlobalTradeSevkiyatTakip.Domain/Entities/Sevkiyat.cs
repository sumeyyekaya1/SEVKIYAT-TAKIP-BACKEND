using GlobalTradeSevkiyatTakip.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Domain.Entities
{
    public class Sevkiyat : BaseModel
    {
       public string? ProjeNo { get; set; }
       public DateTime? SevkiyatTarih { get; set; }
       public string? SevkiyatAdres { get; set; }
       public string? AracPlaka { get; set; }
       public string? SoforAdSoyad { get; set; }
       public decimal? NetMaliyet { get; set; }
       public decimal? Kar { get; set; }
       public decimal? Zarar { get; set; }
       public ICollection<SevkiyatDetay>? SevkiyatDetay { get; set; }
    }
}
