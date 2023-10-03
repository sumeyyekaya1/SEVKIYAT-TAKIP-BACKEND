using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs
{
    public class HizmetEntegreDto : BaseModelDto
    {
        public Int64? WolvoxBlKodu { get; set; }
        public string? HizmetAdi { get; set; }
    }
}
