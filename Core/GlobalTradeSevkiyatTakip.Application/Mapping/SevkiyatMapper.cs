using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.SevkiyatDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Mapping
{
    public class SevkiyatMapper:Profile
    {
        public SevkiyatMapper()
        {
            CreateMap<Sevkiyat, SevkiyatInsertDto>().ReverseMap();
            CreateMap<Sevkiyat, SevkiyatListDto>().ReverseMap();

            CreateMap<SevkiyatDetay, SevkiyatDetayInsertDto>().ReverseMap();
            CreateMap<SevkiyatDetay, SevkiyatDetayListDto>().ReverseMap();
        }
    }
}
