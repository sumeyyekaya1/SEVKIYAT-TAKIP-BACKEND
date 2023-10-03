using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface ICariServis
    {
        Task<PagedApiResponse<List<CariListDto>>> GetPagedListAsync(PagedRequestParam pagedParam);
        Task<List<CariListDto>> GetAllAsync();
        Task<CariListDto> GetByIdAsync(int id);
        Task<CariListDto> AddAsync(CariInsertDto dto);
        Task<List<EntegreResDto>> AddWolvoxAsync(List<CariEntegreDto> dto);
        Task<CariListDto> RemoveAsync(CariInsertDto dto);
        Task<CariListDto> RemoveAsync(int id);
        Task<CariListDto> UpdateAsync(CariInsertDto dto);
        Task<int> ToplamCariAdet();
    }
}
