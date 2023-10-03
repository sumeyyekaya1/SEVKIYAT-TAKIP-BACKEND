using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;

namespace GlobalTradeSevkiyatTakip.Application.Mapping
{
    public class EntegreMapper : Profile
    {
        public EntegreMapper()
        {
            CreateMap<Entegre, EntegreKullaniciResDto>().ReverseMap();
        }
    }
}
