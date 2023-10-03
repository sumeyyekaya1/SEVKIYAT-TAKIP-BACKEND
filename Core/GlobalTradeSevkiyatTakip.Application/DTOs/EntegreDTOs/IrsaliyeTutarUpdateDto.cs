using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs
{
    public class IrsaliyeTutarUpdateDto
    {
        public Int64? WolvoxBlKodu { get; set; }
        public decimal? FaturaTutar { get; set; }
        public string? WolvoxDovizBirim { get; set; }
        public decimal? WolvoxDovizBirimTutar { get; set; }
    }
}
