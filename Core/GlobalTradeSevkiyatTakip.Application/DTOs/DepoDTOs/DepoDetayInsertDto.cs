using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.StokDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.DepoDTOs
{
    public class DepoDetayInsertDto : BaseModelDto
    {
        public int? DepoId { get; set; }
        public int? StokId { get; set; }
        public int? CariId { get; set; }
        public int? Adet { get; set; }
        public DateTime? TarihSaat { get; set; }
        public string? Aciklama { get; set; }
    }
}
