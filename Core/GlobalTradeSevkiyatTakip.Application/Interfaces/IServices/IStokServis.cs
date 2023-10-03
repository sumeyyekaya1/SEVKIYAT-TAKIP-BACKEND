using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.StokDTOs;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface IStokServis
    {
        Task<PagedApiResponse<List<StokListDto>>> GetPagedListAsync(PagedRequestParam pagedParam);
        Task<List<StokListDto>> GetAllAsync();
        Task<StokListDto> GetByIdAsync(int id);
        Task<StokListDto> AddAsync(StokInsertDto dto);
        Task<List<EntegreResDto>> AddWolvoxAsync(List<StokEntegreDto> dto);
    }
}
