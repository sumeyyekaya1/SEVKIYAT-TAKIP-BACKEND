using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs
{
    public class DovizEntegreDto
    {
        public Int64? WolvoxBlKodu { get; set; }
        public string? DovizBirim { get; set; }
        public decimal? AlisFiyat { get; set; }
        public decimal? SatisFiyat { get; set; }
        public DateTime? Tarih { get; set; }
        public string? OzelKod { get; set; }
    }
}
