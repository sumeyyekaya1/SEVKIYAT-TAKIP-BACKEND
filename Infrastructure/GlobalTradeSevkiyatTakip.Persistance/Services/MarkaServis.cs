using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.MarkaDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Persistance.Services
{
    public class MarkaServis : IMarkaServis
    {
        private readonly IUOW unit;
        private readonly IMapper mapper;
        public MarkaServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        public Task<MarkaListDto> AddAsync(MarkaInsertDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<List<MarkaListDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MarkaListDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedApiResponse<List<MarkaListDto>>> GetPagedListAsync(PagedRequestParam pagedParam)
        {
            throw new NotImplementedException();
        }

        public Task<MarkaListDto> RemoveAsync(MarkaInsertDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<MarkaListDto> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MarkaListDto> UpdateAsync(MarkaInsertDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
