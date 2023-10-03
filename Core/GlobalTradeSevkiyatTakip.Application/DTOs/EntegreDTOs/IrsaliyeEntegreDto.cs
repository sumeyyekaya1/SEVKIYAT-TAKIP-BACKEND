using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs
{
    public class IrsaliyeEntegreDto : BaseModelDto
    {
        public Int64? WolvoxBlKodu { get; set; }
        public string? OzelKod { get; set; }
        public DateTime? Tarih { get; set; }
        public DateTime? SevkTarih { get; set; }
        public Int64? WolvoxCariBlKodu { get; set; }
        public string? TicariUnvan { get; set; }
        public string? AdiSoyadi { get; set; }
        public string? DovizBirim { get; set; }
        public string? VergiNo { get; set; }
        public string? VergiAdresi { get; set; }
        public string? Telefon { get; set; }
        public string? Adres { get; set; }
        public string? IrsaliyeNo { get; set; }
        public int? IrsaliyeTur { get; set; }
    }
}
