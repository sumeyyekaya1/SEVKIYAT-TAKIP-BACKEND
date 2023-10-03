using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.HizmetDTOs;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface IHizmetServis
    {
        Task<PagedApiResponse<List<HizmetListDto>>> GetPagedListAsync(PagedRequestParam pagedParam);
        Task<List<HizmetListDto>> GetAllAsync();
        Task<List<EntegreResDto>> AddWolvoxAsync(List<HizmetEntegreDto> dto);
        Task<HizmetListDto> GetByIdAsync(int id);
        Task<HizmetListDto> AddAsync(HizmetInsertDto dto);
        Task<HizmetListDto> RemoveAsync(HizmetInsertDto dto);
        Task<HizmetListDto> RemoveAsync(int id);
        Task<HizmetListDto> UpdateAsync(HizmetInsertDto dto);
    }
}
