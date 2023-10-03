using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.StokDTOs;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Mapping
{
    public class StokMapper:Profile
    {
        public StokMapper()
        {
            CreateMap<Stok, StokInsertDto>().ReverseMap();
            CreateMap<Stok, StokListDto>().ReverseMap();
        }
    }
}
