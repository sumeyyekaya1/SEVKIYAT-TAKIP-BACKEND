using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs
{
    public class CariInsertDto : BaseModelDto
    {
        public string? CariKodu { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? TicariUnvan { get; set; }
        public string? Email { get; set; }
        public string? TcNo { get; set; }
        public string? VergiDairesi { get; set; }
        public string? VergiNo { get; set; }
        public bool? Aktifmi { get; set; }
    }
}
