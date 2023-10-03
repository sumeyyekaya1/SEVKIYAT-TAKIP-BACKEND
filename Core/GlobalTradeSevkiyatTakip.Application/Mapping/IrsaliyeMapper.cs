using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.IrsaliyeDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Mapping
{
    public class IrsaliyeMapper : Profile
    {
        public IrsaliyeMapper()
        {
            CreateMap<Irsaliye, IrsaliyeInsertDto>().ReverseMap();
            CreateMap<Irsaliye, IrsaliyeListDto>().ReverseMap();

            CreateMap<IrsaliyeDetay, IrsaliyeDetayInsertDto>().ReverseMap();
            CreateMap<IrsaliyeDetay, IrsaliyeDetayListDto>().ReverseMap();

            CreateMap<IrsaliyeInsertDto, IrsaliyeListDto>().ReverseMap();
        }
    }
}
