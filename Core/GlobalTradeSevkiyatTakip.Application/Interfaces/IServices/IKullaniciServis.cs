using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.AuthDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.KullaniciDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.MarkaDTOs;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface IKullaniciServis
    {
        Task<PagedApiResponse<List<KullaniciListDto>>> GetPagedListAsync(PagedRequestParam pagedParam);
        Task<List<KullaniciListDto>> GetAllAsync();
        Task<KullaniciListDto> GetByIdAsync(int id);
        Task<KullaniciListDto> AddAsync(KullaniciInsertDto dto);
        Task<KullaniciListDto> RemoveAsync(KullaniciInsertDto dto);
        Task<KullaniciListDto> RemoveAsync(int id);
        Task<KullaniciListDto> UpdateAsync(KullaniciUpdateDto dto);
        Task<KullaniciListDto> UpdatePassword(int id, KullaniciParolaUpdateDto model);
        Task<UserLoginResDto<KullaniciListDto>> LoginAsyn(UserLoginReqDto dto);
    }
}
