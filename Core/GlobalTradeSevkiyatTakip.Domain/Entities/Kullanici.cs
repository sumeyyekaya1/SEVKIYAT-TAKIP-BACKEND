using GlobalTradeSevkiyatTakip.Domain.BaseEntities;
using GlobalTradeSevkiyatTakip.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalTradeSevkiyatTakip.Domain.Entities
{
    public class Kullanici : BaseModel
    {
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? Email { get; set; }
        public string? Parola { get; set; }
        public KullaniciRolEnum? Rol { get; set; }

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
