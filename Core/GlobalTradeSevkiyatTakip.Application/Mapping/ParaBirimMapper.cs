using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.DepoDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.ParaBirimDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Mapping
{
    public class ParaBirimMapper : Profile
    {
        public ParaBirimMapper()
        {
            CreateMap<ParaBirim, ParaBirimListDto>().ReverseMap();
        }
    }
}
