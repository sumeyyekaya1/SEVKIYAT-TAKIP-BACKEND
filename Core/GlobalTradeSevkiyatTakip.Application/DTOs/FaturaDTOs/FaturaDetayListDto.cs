using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.HizmetDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.StokDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.FaturaDTOs
{
    public class FaturaDetayListDto : BaseModelDto
    {
        public Int64? WolvoxBlKodu { get; set; }
        public int? StokId { get; set; }
        public StokListDto? Stok { get; set; }
        public int? HizmetId { get; set; }
        public HizmetListDto? Hizmet { get; set; }
        public decimal? Miktar { get; set; }
        public decimal? ToplamTutar { get; set; }
    }
}
