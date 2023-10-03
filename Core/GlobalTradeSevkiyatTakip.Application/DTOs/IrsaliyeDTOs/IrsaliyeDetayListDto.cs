using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.HizmetDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.StokDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.IrsaliyeDTOs
{
    public class IrsaliyeDetayListDto:BaseModelDto
    {
        public int? IrsaliyeId { get; set; }
        public IrsaliyeListDto? Irsaliye { get; set; }
        public int? StokId { get; set; }
        public StokListDto? Stok { get; set; }
        public int? HizmetId { get; set; }
        public HizmetListDto? Hizmet { get; set; }
        public int? Miktar { get; set; }
        public bool? TakimDurum { get; set; }
        public string? Icindekiler { get; set; }
        public string? Olculer { get; set; }
        public string? KapNo { get; set; }
        public string? PaketSekil { get; set; }
        public int? PaketIciAdet { get; set; }
        public int? ToplamPaketIciAdet { get; set; }
        public int? KapAdet { get; set; }
        public int? ToplamKapAdet { get; set; }
        public string? TedarikFirma { get; set; }
        public string? UrunIcerik { get; set; }
        public decimal? UrunBurutAgirlik { get; set; }
        public decimal? UrunNetAgirlik { get; set; }
    }
}
