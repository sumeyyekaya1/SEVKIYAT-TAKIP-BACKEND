using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.DepoDTOs
{
    public class DepoListDto : BaseModelDto
    {
        public Int64? WolvoxBlKodu { get; set; }
        public string? DepoAd { get; set; }
        public string? DepoAdres { get; set; }
        public string? DepoIletisim { get; set; }
        public string? DepoYetkili { get; set; }
    }
}
