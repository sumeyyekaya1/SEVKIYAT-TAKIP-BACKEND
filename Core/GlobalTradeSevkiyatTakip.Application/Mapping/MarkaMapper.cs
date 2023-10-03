using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.DepoDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.MarkaDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Mapping
{
    public class MarkaMapper : Profile
    {
        public MarkaMapper()
        {
            CreateMap<Marka, MarkaListDto>().ReverseMap();
            CreateMap<Depo, MarkaInsertDto>().ReverseMap();
        }
    }
}
