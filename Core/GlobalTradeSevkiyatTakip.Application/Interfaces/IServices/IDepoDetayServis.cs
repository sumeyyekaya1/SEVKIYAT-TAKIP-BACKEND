using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.DepoDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.RaporDTOs;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface IDepoDetayServis
    {
        Task<PagedApiResponse<List<DepoDetayListDto>>> GetPagedListAsync(PagedRequestParam pagedParam, int depoId);
        Task<List<DepoDetayListDto>> GetAllAsync();
        Task<DepoDetayListDto> GetByIdAsync(int id);
        Task<DepoDetayListDto> AddAsync(DepoDetayInsertDto dto);
    }
}
