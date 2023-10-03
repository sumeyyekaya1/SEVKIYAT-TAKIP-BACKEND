using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.FaturaDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Mapping
{
    public class FaturaMapper : Profile
    {
        public FaturaMapper()
        {
            CreateMap<Fatura, FaturaListDto>().ReverseMap();
            CreateMap<FaturaDetay, FaturaDetayListDto>().ReverseMap();
        }
    }
}
