using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.RaporDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.SevkiyatDTOs;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface ISevkiyatServis
    {
        Task<List<SevkiyatListDto>> GetAllAsync();
        Task<PagedApiResponse<List<SevkiyatListDto>>> GetPagedListAsync(PagedRequestParam pagedParam);
        Task<SevkiyatListDto> GetByIdAsync(int id);
        Task<SevkiyatListDto> AddAsync(SevkiyatInsertDto dto);
        Task<SevkiyatListDto> RemoveAsync(SevkiyatInsertDto dto);
        Task<SevkiyatListDto> RemoveAsync(int id);
        Task<SevkiyatListDto> UpdateAsync(SevkiyatInsertDto dto);
        Task<List<SevkiyatCekiListeRaporDto>> GetAllReportAsync(int sevkiyatId);
        Task<byte[]> CreateReport(List<SevkiyatCekiListeRaporDto> itemList);
        Task<List<SevkiyatMaliyetRaporDto>> ReportForMaliyetAsync(int sevkiyatId);
        Task<byte[]> CreateReportForMaliyet(List<SevkiyatMaliyetRaporDto> itemList);
        Task<List<SevkiyatMaliyetRaporDto>> DonemSevkiyatReportAsync(DateTime baslangicTarih, DateTime bitisTarih);
        Task<int> ToplamSevkiyatAdet();
        Task<List<int>> AylikSevkiyatAdet();
        Task<SevkiyatKurDonusumDto> DovizBirimDonusum(int sevkiyatId, int paraBirimId);
    }
}
