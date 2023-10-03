using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.FaturaDTOs;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface IFaturaServis
    {
        Task<PagedApiResponse<List<FaturaListDto>>> GetPagedListAsync(PagedRequestParam pagedParam);
        Task<List<FaturaListDto>> GetAllAsync();
        Task<FaturaListDto> GetByIdAsync(int id);
        Task<List<EntegreResDto>> AddWolvoxAsync(List<FaturaEntegreViewModelDto> dto);
    }
}
