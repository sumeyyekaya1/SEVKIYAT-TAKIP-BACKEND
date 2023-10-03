using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.DepoDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.RaporDTOs;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface IDepoServis
    {
        Task<PagedApiResponse<List<DepoListDto>>> GetPagedListAsync(PagedRequestParam pagedParam);
        Task<List<DepoListDto>> GetAllAsync();
        Task<DepoListDto> GetByIdAsync(int id);
        Task<DepoListDto> AddAsync(DepoInsertDto dto);
        Task<byte[]> CreateReport(List<DepoRaporDto> itemList);
        Task<List<DepoRaporDto>> GetAllReportAsync(int depoId);
    }
}
