using GlobalTradeSevkiyatTakip.Domain.BaseEntities;
using GlobalTradeSevkiyatTakip.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Domain.Entities
{
    public class Irsaliye : BaseModel
    {
        public Int64? WolvoxBlKodu { get; set; }
        public string? ProjeNo { get; set; }
        public string? IrsaliyeNo { get; set; }
        public DateTime? IrsaliyeTarih { get; set; }
        public DateTime? SevkTarih { get; set; }
        public string? SevkAdres { get; set; }
        public int? CariId { get; set; }
        public Cari? Cari { get; set; }
        public int? DovizId { get; set; }
        public Doviz? Doviz { get; set; }
        public IrsaliyeSevkDurumEnum? SevkDurum { get; set; } = IrsaliyeSevkDurumEnum.Beklemede;
        public IrsaliyeTurEnum? IrsaliyeTur { get; set; } = IrsaliyeTurEnum.Alis;
        public IrsaliyeFaturaDurumEnum? FaturaDurum { get; set; } = IrsaliyeFaturaDurumEnum.FaturaBekliyor;
        public WolvoxDurumEnum? WolvoxGonderimDurum { get; set; } = WolvoxDurumEnum.WolvoxaGonderilecek;
        public EvrakTurEnum? EvrakTur { get; set; } = EvrakTurEnum.Irsaliye;
        public virtual ICollection<IrsaliyeDetay>? IrsaliyeDetay { get; set; }
    }
}
