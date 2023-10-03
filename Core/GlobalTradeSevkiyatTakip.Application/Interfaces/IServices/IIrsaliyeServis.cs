using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.IrsaliyeDTOs;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface IIrsaliyeServis
    {
        Task<List<IrsaliyeListDto>> GetAllAsync();
        Task<PagedApiResponse<List<IrsaliyeListViewModelDto>>> GetPagedListAsync(PagedRequestParam pagedParam);
        Task<PagedApiResponse<List<IrsaliyeListDto>>> GetPagedListFaturalananAsync(PagedRequestParam pagedParam);
        Task<IrsaliyeListDto> GetByIdAsync(int id);
        Task<IrsaliyeListViewModelDto> AddAsync(IrsaliyeViewModelDto dto);
        Task<List<IrsaliyeEntegreViewModelDto>> SendWolvoxYeniIrsaliye();
        Task<List<EntegreResDto>> AddWolvoxAsync(List<IrsaliyeEntegreViewModelDto> dto);
        Task UpdateWolvoxBlKodu(List<BlKoduUpdateDto> dto);
        Task<IrsaliyeListDto> RemoveAsync(IrsaliyeInsertDto dto);
        Task<IrsaliyeListDto> RemoveAsync(int id);
        Task<IrsaliyeListViewModelDto> UpdateAsync(IrsaliyeInsertDto dto);
        Task<List<EntegreResDto>> FaturalanmayanIrsaliyeListAsync();
        Task<int> ToplamIrsaliyeAdet();
    }
}
