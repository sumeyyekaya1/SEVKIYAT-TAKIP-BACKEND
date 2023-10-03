using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.FaturaDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.IrsaliyeDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.SevkiyatDTOs
{
    public class SevkiyatDetayListDto:BaseModelDto
    {
        public int? SevkiyatId { get; set; }
        public SevkiyatListDto? Sevkiyat { get; set; }
        public int? IrsaliyeId { get; set; }
        public IrsaliyeListDto? Irsaliye { get; set; }
        public int? FaturaId { get; set; }
        public FaturaListDto? Fatura { get; set; }
    }
}
