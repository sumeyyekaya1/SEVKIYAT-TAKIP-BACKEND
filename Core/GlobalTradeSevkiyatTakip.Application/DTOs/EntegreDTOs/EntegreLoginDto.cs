using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs
{
    public class EntegreLoginDto
    {
        public string Email { get; set; }
        public string Parola { get; set; }
        public bool BeniHatirla { get; set; }
    }
}
