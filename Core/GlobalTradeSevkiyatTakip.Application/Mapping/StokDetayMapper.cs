using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.StokDetayDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.StokDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Mapping
{
    public class StokDetayMapper : Profile
    {
        public StokDetayMapper()
        {
            CreateMap<StokDetay, StokDetayListDto>().ReverseMap();
            CreateMap<StokDetay, StokDetayEntegreDto>().ReverseMap();
        }
    }
}
