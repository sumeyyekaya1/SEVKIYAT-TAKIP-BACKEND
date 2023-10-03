using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.KullaniciDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;

namespace GlobalTradeSevkiyatTakip.Application.Mapping
{
    public class KullaniciMapper:Profile
    {
        public KullaniciMapper()
        {
            CreateMap<Kullanici, KullaniciInsertDto>().ReverseMap();
            CreateMap<Kullanici, KullaniciListDto>().ReverseMap();
            CreateMap<Kullanici, KullaniciUpdateDto>().ReverseMap();

        }
    }
}
