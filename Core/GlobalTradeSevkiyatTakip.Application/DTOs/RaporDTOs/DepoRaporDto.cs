using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.RaporDTOs
{
    public class DepoRaporDto
    {
        public string TicariUnvan { get; set; }
        public string DepoAd { get; set; }
        public string DepoAdres { get; set; }
        public string StokAdi { get; set; }
        public int Adet { get; set; }
        public DateTime GirisTarih { get; set; }
    }
}
