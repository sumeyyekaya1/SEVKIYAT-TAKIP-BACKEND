using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.SevkiyatNotDTOs;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface ISevkiyatNotServis
    {
        Task<PagedApiResponse<List<SevkiyatNotInsertResDto>>> GetPagedListAsync(PagedRequestParam pagedParam);
        Task<List<SevkiyatNotInsertResDto>> GetAllAsync();
        Task<SevkiyatNotInsertResDto> GetByIdAsync(int id);
        Task<SevkiyatNotInsertResDto> AddAsync(SevkiyatNotInsertReqDto dto, int kullaniciId);
        Task<SevkiyatNotInsertResDto> RemoveAsync(SevkiyatNotInsertReqDto dto);
        Task<SevkiyatNotInsertResDto> RemoveAsync(int id);
        Task<SevkiyatNotInsertResDto> UpdateAsync(SevkiyatNotInsertReqDto dto);
    }
}
