using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.KullaniciDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.SevkiyatNotDTOs
{
    public class SevkiyatNotInsertResDto : BaseModelDto
    {
        public string? Metin { get; set; }
        public int? KullaniciId { get; set; }
        public KullaniciListDto? Kullanici { get; set; }
    }
}
