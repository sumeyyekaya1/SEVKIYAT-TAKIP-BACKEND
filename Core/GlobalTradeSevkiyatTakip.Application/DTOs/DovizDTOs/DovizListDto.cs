using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.DovizDTOs
{
    public class DovizListDto : BaseModelDto
    {
        public string? DovizBirim { get; set; }
        public decimal? AlisFiyat { get; set; }
        public decimal? SatisFiyat { get; set; }
        public DateTime? Tarih { get; set; }
    }
}
