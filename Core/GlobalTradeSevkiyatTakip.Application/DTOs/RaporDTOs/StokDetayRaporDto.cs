using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.RaporDTOs
{
    public class StokDetayRaporDto
    {
        public string? StokAdi { get; set; }
        public string? CariAdi { get; set; }
        public string? DepoAdi { get; set; }
        public decimal? Miktar { get; set; }
    }
}
