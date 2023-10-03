using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.RaporDTOs
{
    public class SevkiyatCekiListeRaporDto
    {
        public string? ProjeNo { get; set; }
        public string? MusteriKod { get; set; }
        public string? UrunAciklama { get; set; }
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
        public string? Marka { get; set; }
        public string? UrunIcerik { get; set; }
        public decimal? UrunBurutAgirlik { get; set; }
        public decimal? UrunNetAgirlik { get; set; }
    }
}
