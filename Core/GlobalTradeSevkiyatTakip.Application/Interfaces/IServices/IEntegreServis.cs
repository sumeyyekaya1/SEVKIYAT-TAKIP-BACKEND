using GlobalTradeSevkiyatTakip.Application.DTOs.AuthDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface IEntegreServis
    {
        Task<List<EntegreKullaniciResDto>> GetAllAsync();
        Task<EntegreKullaniciResDto> AddAsync(EntegreKullaniciReqDto dto);
        Task<EntegreKullaniciResDto> RemoveAsync(EntegreKullaniciReqDto dto);
        Task<EntegreKullaniciResDto> RemoveAsync(int id);
        Task<EntegreKullaniciResDto> UpdateAsync(EntegreKullaniciReqDto dto);
        Task<UserLoginResDto<EntegreKullaniciResDto>> TokenExpiredControl(string token);
        Task<UserLoginResDto<EntegreKullaniciResDto>> LoginAsyn(EntegreLoginDto dto);
    }
}
