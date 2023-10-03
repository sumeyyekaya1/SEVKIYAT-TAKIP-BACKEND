using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.RenkDTOs;
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
    public class RenkServis : IRenkServis
    {
        private readonly IUOW unit;
        private readonly IMapper mapper;
        public RenkServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        public Task<RenkListDto> AddAsync(RenkInsertDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<List<RenkListDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RenkListDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedApiResponse<List<RenkListDto>>> GetPagedListAsync(PagedRequestParam pagedParam)
        {
            throw new NotImplementedException();
        }

        public Task<RenkListDto> RemoveAsync(RenkInsertDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<RenkListDto> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RenkListDto> UpdateAsync(RenkInsertDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
