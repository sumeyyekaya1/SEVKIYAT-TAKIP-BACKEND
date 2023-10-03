using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs
{
    public class FaturaEntegreDto : BaseModelDto
    {
        public Int64? WolvoxBlKodu { get; set; }
        public Int64? WolvoxCariBlKodu { get; set; }
        public DateTime? Tarih { get; set; }
        public string? FaturaNo { get; set; }
        public string? DovizBirim { get; set; }
        public string? OzelKod { get; set; }
        public decimal? WolvoxFaturaTutar { get; set; }
        public decimal? WolvoxDolarTutar { get; set; }
        public int? FaturaTip { get; set; }

    }
}
