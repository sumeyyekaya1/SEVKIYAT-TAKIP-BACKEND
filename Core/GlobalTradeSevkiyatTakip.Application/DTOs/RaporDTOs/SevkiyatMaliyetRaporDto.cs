using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.RaporDTOs
{
    public class SevkiyatMaliyetRaporDto
    {
        public string? HizmetAd { get; set; }
        public string? ProjeNo { get; set; }
        public string? HizmetTur { get; set; }
        public decimal? Miktar { get; set; }
        public decimal? Tutar { get; set; }
        public decimal? AlisToplamTutar { get; set; }
        public decimal? SatisToplamTutar { get; set; }
        public decimal? ToplamSevkiyatTutar { get; set; }
    }
}
