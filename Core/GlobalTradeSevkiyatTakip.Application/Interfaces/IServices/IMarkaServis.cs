using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.MarkaDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.RenkDTOs;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface IMarkaServis
    {
        Task<PagedApiResponse<List<MarkaListDto>>> GetPagedListAsync(PagedRequestParam pagedParam);
        Task<List<MarkaListDto>> GetAllAsync();
        Task<MarkaListDto> GetByIdAsync(int id);
        Task<MarkaListDto> AddAsync(MarkaInsertDto dto);
        Task<MarkaListDto> RemoveAsync(MarkaInsertDto dto);
        Task<MarkaListDto> RemoveAsync(int id);
        Task<MarkaListDto> UpdateAsync(MarkaInsertDto dto);
    }
}
