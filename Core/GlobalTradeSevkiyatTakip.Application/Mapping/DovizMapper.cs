using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.DovizDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Mapping
{
    public class DovizMapper : Profile
    {
        public DovizMapper()
        {

            CreateMap<Doviz, DovizListDto>().ReverseMap();
            CreateMap<Doviz, DovizInsertDto>().ReverseMap();
        }
    }
}
