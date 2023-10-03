using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;

namespace GlobalTradeSevkiyatTakip.Application.Mapping
{
    public class CariMapper : Profile
    {
        public CariMapper()
        {
            CreateMap<Cari, CariInsertDto>().ReverseMap();
            CreateMap<Cari, CariListDto>().ReverseMap();
        }
    }
}
