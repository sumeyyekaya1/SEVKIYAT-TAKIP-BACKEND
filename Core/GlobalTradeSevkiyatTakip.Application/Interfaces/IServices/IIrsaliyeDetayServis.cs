using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.DepoDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.IrsaliyeDTOs;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface IIrsaliyeDetayServis
    {
        Task<List<IrsaliyeDetayListDto>> GetAllAsync();
        Task<PagedApiResponse<List<IrsaliyeDetayListDto>>> GetPagedListAsync(PagedRequestParam pagedParam, int irsaliyeId);
        Task<IrsaliyeDetayListDto> GetByIdAsync(int id);
        Task<IrsaliyeDetayListDto> AddAsync(IrsaliyeDetayInsertDto dto);
        Task<IrsaliyeDetayListDto> RemoveAsync(IrsaliyeDetayInsertDto dto);
        Task<IrsaliyeDetayListDto> RemoveAsync(int id);
        Task<IrsaliyeDetayListDto> UpdateAsync(IrsaliyeDetayInsertDto dto);

    }
}
