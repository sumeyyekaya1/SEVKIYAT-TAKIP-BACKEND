using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs
{
    public class FaturaDetayEntegreDto : BaseModelDto
    {
        public Int64? WolvoxBlKodu { get; set; }
        public Int64? WolvoxStokBlKodu { get; set; }
        public Int64? WolvoxHizmetBlKodu { get; set; }
        public decimal? Miktar { get; set; }
        public decimal? ToplamTutar { get; set; }
        public string? DovizBirim { get; set; }
    }
}
