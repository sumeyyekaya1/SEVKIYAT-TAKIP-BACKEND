using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.RenkDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.SevkiyatNotDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Mapping
{
    public class SevkiyatNotMapper : Profile
    {
        public SevkiyatNotMapper()
        {
            CreateMap<SevkiyatNot, SevkiyatNotInsertReqDto>().ReverseMap();
            CreateMap<SevkiyatNot, SevkiyatNotInsertResDto>().ReverseMap();
        }
    }
}
