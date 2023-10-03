using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.DovizDTOs;
using GlobalTradeSevkiyatTakip.Domain.Enums;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.FaturaDTOs
{
    public class FaturaListDto : BaseModelDto
    {
        public int? CariId { get; set; }
        public CariListDto? Cari { get; set; }
        public DateTime? Tarih { get; set; }
        public string? FaturaNo { get; set; }
        public int? DovizId { get; set; }
        public DovizListDto? Doviz { get; set; }
        public string? ProjeNo { get; set; }
        public decimal? WolvoxFaturaTutar { get; set; }
        public decimal? WolvoxDolarTutar { get; set; }
        public decimal? WolvoxDovizBirimTutar { get; set; }
        public FaturaTurEnum? FaturaTur { get; set; }
        public ICollection<FaturaDetayListDto>? Detay { get; set; }
    }
}
