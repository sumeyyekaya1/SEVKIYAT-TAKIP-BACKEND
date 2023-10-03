using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.DepoDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.ParaBirimDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Persistance.Services
{
    public class ParaBirimServis : IParaBirimServis
    {
        private readonly IUOW unit;
        private readonly IMapper mapper;
        public ParaBirimServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task<List<ParaBirimListDto>> GetAllAsync()
        {
            try
            {
                var entity = await unit.ParaBirimRepo.GetAllAsync().ToListAsync();
                return mapper.Map<List<ParaBirimListDto>>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
