using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.DepoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.StokDTOs
{
    public class StokListDto : BaseModelDto
    {
        public string? StokKod { get; set; }
        public string? Barkod { get; set; }
        public Int64? WolvoxBlKodu { get; set; }
        public string? StokAdi { get; set; }
        public int DepoId { get; set; }
        public DepoListDto? Depo { get; set; }
        public int? KdvOran { get; set; }
        public int? IskontoOran { get; set; }
        public decimal? Agirlik { get; set; }
        public decimal? DesiMiktari { get; set; }
        public string? DovizBirim { get; set; }
    }
}
