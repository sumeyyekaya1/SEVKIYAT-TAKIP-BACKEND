using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.HizmetDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.SevkiyatDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Mapping
{
    public class HizmetMapper : Profile
    {
        public HizmetMapper()
        {
            CreateMap<Hizmet, HizmetInsertDto>().ReverseMap();
            CreateMap<Hizmet, HizmetListDto>().ReverseMap();
        }
    }
}
