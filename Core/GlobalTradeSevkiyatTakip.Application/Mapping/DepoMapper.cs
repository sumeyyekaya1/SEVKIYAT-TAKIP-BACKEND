using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.DepoDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Mapping
{
    public class DepoMapper : Profile
    {
        public DepoMapper()
        {
            CreateMap<Depo, DepoInsertDto>().ReverseMap();
            CreateMap<Depo, DepoListDto>().ReverseMap();

            CreateMap<DepoDetay, DepoDetayInsertDto>().ReverseMap();
            CreateMap<DepoDetay, DepoDetayListDto>().ReverseMap();
        }
    }
}
