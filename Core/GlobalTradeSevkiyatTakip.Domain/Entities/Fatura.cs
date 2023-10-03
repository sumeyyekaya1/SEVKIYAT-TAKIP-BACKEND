using GlobalTradeSevkiyatTakip.Domain.BaseEntities;
using GlobalTradeSevkiyatTakip.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Domain.Entities
{
    public class Fatura : BaseModel
    {
        public Int64? WolvoxBlKodu { get; set; }
        public int? CariId { get; set; }
        public Cari? Cari { get; set; }
        public DateTime? Tarih { get; set; }
        public string? FaturaNo { get; set; }
        public int? DovizId { get; set; }
        public Doviz? Doviz { get; set; }
        public string? ProjeNo { get; set; }
        public decimal? WolvoxFaturaTutar { get; set; }
        public decimal? WolvoxDolarTutar { get; set; }
        public decimal? WolvoxDovizBirimTutar { get; set; }
        public FaturaTurEnum? FaturaTur { get; set; }
        public EvrakTurEnum? EvrakTur { get; set; } = EvrakTurEnum.Fatura;
        public ICollection<FaturaDetay>? Detay { get; set; }
    }
}
