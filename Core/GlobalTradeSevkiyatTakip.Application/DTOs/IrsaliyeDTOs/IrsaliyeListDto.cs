using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.DovizDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using GlobalTradeSevkiyatTakip.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.IrsaliyeDTOs
{
    public class IrsaliyeListDto : BaseModelDto
    {
        public string? IrsaliyeNo { get; set; }
        public string? ProjeNo { get; set; }
        public DateTime? IrsaliyeTarih { get; set; }
        public DateTime? SevkTarih { get; set; }
        public string? SevkAdres { get; set; }
        public int? CariId { get; set; }
        public CariListDto? Cari { get; set; }
        public int? DovizId { get; set; }
        public DovizListDto? Doviz { get; set; }
        public IrsaliyeSevkDurumEnum? SevkDurum { get; set; }
        public IrsaliyeFaturaDurumEnum? FaturaDurum { get; set; }
        public IrsaliyeTurEnum? IrsaliyeTur { get; set; }
        public virtual ICollection<IrsaliyeDetayInsertDto>? IrsaliyeDetay { get; set; }
    }
}
