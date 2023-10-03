using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.HizmetDTOs
{
    public class HizmetInsertDto : BaseModelDto
    {
        public string? HizmetAdi { get; set; }
    }
}
