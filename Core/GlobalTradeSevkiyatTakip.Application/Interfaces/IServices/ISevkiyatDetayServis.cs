using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.IrsaliyeDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.SevkiyatDTOs;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface ISevkiyatDetayServis
    {
        Task<PagedApiResponse<List<SevkiyatDetayListDto>>> GetPagedListForFaturaAsync(PagedRequestParam pagedParam, int sevkiyatId);
        Task<PagedApiResponse<List<SevkiyatDetayListDto>>> GetPagedListForIrsaliyeAsync(PagedRequestParam pagedParam, int sevkiyatId);
        Task<List<SevkiyatDetayListDto>> GetAllAsync();
        Task<SevkiyatDetayListDto> GetByIdAsync(int id);
        Task<SevkiyatDetayListDto> AddAsync(SevkiyatDetayInsertDto dto);
        Task<SevkiyatDetayListDto> RemoveAsync(SevkiyatDetayInsertDto dto);
        Task<SevkiyatDetayListDto> RemoveAsync(int id);
        Task<SevkiyatDetayListDto> UpdateAsync(SevkiyatDetayInsertDto dto);
    }
}
