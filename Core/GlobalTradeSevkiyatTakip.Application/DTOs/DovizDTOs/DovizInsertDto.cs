using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.DovizDTOs
{
    public class DovizInsertDto : BaseModelDto
    {
        public Int64? WolvoxBlKodu { get; set; }
        public string? DovizBirim { get; set; }
        public decimal? AlisFiyat { get; set; }
        public decimal? SatisFiyat { get; set; }
        public DateTime? Tarih { get; set; }
        public string? OzelKod { get; set; }
    }
}
