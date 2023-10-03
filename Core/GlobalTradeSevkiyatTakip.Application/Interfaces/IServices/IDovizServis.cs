using GlobalTradeSevkiyatTakip.Application.DTOs.DovizDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface IDovizServis
    {
        Task<List<DovizListDto>> GetAllAsync();
        Task<DovizListDto> GetByIdAsync(int id);
        Task<DovizListDto> AddAsync(DovizInsertDto dto);
        Task<List<EntegreResDto>> AddWolvoxAsync(List<DovizEntegreDto> dto);
        Task<DovizListDto> RemoveAsync(DovizInsertDto dto);
        Task<DovizListDto> RemoveAsync(int id);
        Task<DovizListDto> UpdateAsync(DovizInsertDto dto);
    }
}
