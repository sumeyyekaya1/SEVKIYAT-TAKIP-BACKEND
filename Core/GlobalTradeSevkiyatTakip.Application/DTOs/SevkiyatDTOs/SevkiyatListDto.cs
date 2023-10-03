using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.SevkiyatDTOs
{
    public class SevkiyatListDto : BaseModelDto
    {
        public string? ProjeNo { get; set; }
        public DateTime? SevkiyatTarih { get; set; }
        public string? SevkiyatAdres { get; set; }
        public string? AracPlaka { get; set; }
        public string? SoforAdSoyad { get; set; }
        public ICollection<SevkiyatDetayInsertDto>? SevkiyatDetay { get; set; }
    }
}
