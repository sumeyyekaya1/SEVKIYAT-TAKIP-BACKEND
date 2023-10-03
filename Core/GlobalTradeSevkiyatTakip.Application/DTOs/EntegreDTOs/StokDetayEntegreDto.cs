using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs
{
    public class StokDetayEntegreDto
    {
        public Int64? WolvoxBlKodu { get; set; }
        public Int64? WolvoxStokBlKodu { get; set; }
        public string? CariKodu { get; set; }
        public string? DepoAdi { get; set; }
        public decimal? Miktar { get; set; }
        public bool? TutarTuru { get; set; } //1(TRUE) GİRİŞ - 0(FALSE) ÇIKIŞ 
    }
}
