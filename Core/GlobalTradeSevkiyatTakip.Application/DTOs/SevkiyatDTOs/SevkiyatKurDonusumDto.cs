using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.SevkiyatDTOs
{
    public class SevkiyatKurDonusumDto
    {
        public decimal? NetMaliyet { get; set; } = 0;
        public decimal? Kar { get; set; } = 0;
        public decimal? Zarar { get; set; } = 0;
        public string? BirimSimge { get; set; }
    }
}
