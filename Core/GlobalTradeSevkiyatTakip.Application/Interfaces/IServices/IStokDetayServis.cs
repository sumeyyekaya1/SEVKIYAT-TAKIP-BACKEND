using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.RaporDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.StokDetayDTOs;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface IStokDetayServis
    {
        Task<PagedApiResponse<List<StokDetayListDto>>> GetPagedListAsync(PagedRequestParam pagedParam, int depoId);
        Task<List<StokDetayListDto>> GetAllAsync();
        Task<StokDetayListDto> GetByIdAsync(int id);
        Task<List<EntegreResDto>> AddWolvoxAsync(List<StokDetayEntegreDto> dto);
        Task<List<DepoRaporDto>> GetAllReportAsync(int depoId);
        Task<byte[]> CreateReport(List<DepoRaporDto> itemList);
        Task<decimal> ToplamStokAdet();
    }
}
