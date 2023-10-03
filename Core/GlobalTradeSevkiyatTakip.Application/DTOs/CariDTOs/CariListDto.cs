using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs
{
    public class CariListDto : BaseModelDto
    {
        public Int64? WolvoxBlKodu { get; set; }
        public string? CariKodu { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? TicariUnvan { get; set; }
        public string? Email { get; set; }
        public string? TcNo { get; set; }
        public string? VergiDairesi { get; set; }
        public string? VergiNo { get; set; }
        public DateTime? DogumTarih { get; set; }
        public bool? Aktifmi { get; set; }
        [NotMapped]
        public string? AdSoyad
        {
            get
            {
                return Ad + " " + Soyad;
            }
        }
    }
}
