using GlobalTradeSevkiyatTakip.Domain.BaseEntities;
using GlobalTradeSevkiyatTakip.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Domain.Entities
{
    public class Entegre : BaseModel
    {
        public string Email { get; set; }
        public string Parola { get; set; }
        public bool BeniHatirla { get; set; }
        public KullaniciRolEnum Rol { get; set; }
    }
}
