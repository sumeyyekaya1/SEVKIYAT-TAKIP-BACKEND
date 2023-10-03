using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs
{
    public class BaseModelDto
    {
        public int ID { get; set; }
        public DateTime GuncellemeTarih { get; set; } = DateTime.Now;
        public DateTime OlusturmaTarih { get; set; } = DateTime.Now;
        public string OlusturanKullanici { get; set; } = "sd";
        public string GuncelleyenKullanici { get; set; } = "asd";
        public bool? IsDeleted { get; set; } = false;
    }
}
