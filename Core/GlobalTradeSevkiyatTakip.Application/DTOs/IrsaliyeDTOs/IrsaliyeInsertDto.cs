using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.IrsaliyeDTOs
{
    public class IrsaliyeInsertDto : BaseModelDto
    {
        public string? IrsaliyeNo { get; set; }
        public string? ProjeNo { get; set; }
        public DateTime? IrsaliyeTarih { get; set; }
        public DateTime? SevkTarih { get; set; }
        public string? SevkAdres { get; set; }
        public int? CariId { get; set; }

    }
}
