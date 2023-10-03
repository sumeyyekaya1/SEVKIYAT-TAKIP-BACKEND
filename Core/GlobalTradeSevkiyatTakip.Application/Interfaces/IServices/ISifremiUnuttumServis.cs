using GlobalTradeSevkiyatTakip.Application.DTOs.AuthDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.KullaniciDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.SistemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface ISifremiUnuttumServis
    {
        Task<UserLoginResDto<KullaniciListDto>> GenerateForgotPasswordToken(string eposta, DateTime gecerlilikSuresi);
        Task<KullaniciListDto> UpdatePassword(int id, SifremiUnuttumDto model);
    }
}
