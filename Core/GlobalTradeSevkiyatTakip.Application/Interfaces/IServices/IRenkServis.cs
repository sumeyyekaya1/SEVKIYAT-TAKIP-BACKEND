using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.RenkDTOs;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface IRenkServis 
    {
        Task<PagedApiResponse<List<RenkListDto>>> GetPagedListAsync(PagedRequestParam pagedParam);
        Task<List<RenkListDto>> GetAllAsync();
        Task<RenkListDto> GetByIdAsync(int id);
        Task<RenkListDto> AddAsync(RenkInsertDto dto);
        Task<RenkListDto> RemoveAsync(RenkInsertDto dto);
        Task<RenkListDto> RemoveAsync(int id);
        Task<RenkListDto> UpdateAsync(RenkInsertDto dto);
    }
}
