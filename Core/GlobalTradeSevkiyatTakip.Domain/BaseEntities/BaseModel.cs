using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Domain.BaseEntities
{
    public class BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string OlusturanKullanici { get; set; } = "sd";
        public string GuncelleyenKullanici { get; set; } = "asd";
        public DateTime GuncellemeTarih { get; set; } = DateTime.Now;
        public DateTime OlusturmaTarih { get; set; } = DateTime.Now;
        public bool? IsDeleted { get; set; } = false;
    }
}
