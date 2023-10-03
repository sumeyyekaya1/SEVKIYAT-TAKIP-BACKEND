using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.StokDTOs
{
    public class StokInsertDto : BaseModelDto
    {
        public string? StokKod { get; set; }
        public string? Barkod { get; set; }
        public string? StokAdi { get; set; }
        public int? KdvOran { get; set; }
        public int? IskontoOran { get; set; }
        public decimal? Agirlik { get; set; }
        public decimal? DesiMiktari { get; set; }
        public string? DovizBirim { get; set; }
    }
}
