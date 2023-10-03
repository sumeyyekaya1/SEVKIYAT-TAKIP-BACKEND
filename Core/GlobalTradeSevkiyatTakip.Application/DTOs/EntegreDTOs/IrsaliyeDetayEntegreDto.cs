using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs
{
    public class IrsaliyeDetayEntegreDto : BaseModelDto
    {
        public Int64? WolvoxStokBlKodu { get; set; }
        public string? StokAdi { get; set; }
        public string? StokKodu { get; set; }
        public string? Miktari { get; set; }
        public string? DepoAdi { get; set; }
        public Int64? WolvoxHizmetBlKodu { get; set; }
    }
}
