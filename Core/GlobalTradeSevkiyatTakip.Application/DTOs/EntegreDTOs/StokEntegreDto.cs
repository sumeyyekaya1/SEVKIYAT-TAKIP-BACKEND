namespace GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs
{
    public class StokEntegreDto
    {
        public Int64 WolvoxBlKodu { get; set; }
        public string? StokKod { get; init; }
        public string? DepoAdi { get; init; }
        public string? Barkod { get; init; }
        public string? StokAdi { get; init; }
        public string? Renk { get; init; }
        public string? Grup { get; set; }
        public string? Marka { get; init; }
        public string? KdvOran { get; init; }
        public string? IskontoOran { get; init; }
        public string? Agirlik { get; init; }
        public string? DovizBirim { get; init; }
    }
}
