using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using GlobalTradeSevkiyatTakip.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.KullaniciDTOs
{
    public class KullaniciUpdateDto : BaseModelDto
    {
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? Email { get; set; }
        public KullaniciRolEnum? Rol { get; set; }
    }
}
