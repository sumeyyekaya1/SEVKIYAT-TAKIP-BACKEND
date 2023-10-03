using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.AuthDTOs
{
    public class KullaniciParolaUpdateDto
    {
        public string? EskiParola { get; set; }
        public string? YeniParola { get; set; }
        public string? YeniParolaTekrar { get; set; }
    }
}
