using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.DepoDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.RenkDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Mapping
{
    public class RenkMapper : Profile
    {
        public RenkMapper()
        {
            CreateMap<Renk, RenkListDto>().ReverseMap();
            CreateMap<Renk, RenkInsertDto>().ReverseMap();
        }
    }
}
