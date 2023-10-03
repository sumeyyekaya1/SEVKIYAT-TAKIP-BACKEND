using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.AuthDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.KullaniciDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.SistemDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Application.Utilities.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Persistance.Services
{
    public class SifremiUnuttumServis : ISifremiUnuttumServis
    {
        private readonly IUOW unit;
        private readonly IMapper mapper;

        public SifremiUnuttumServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        public async Task<UserLoginResDto<KullaniciListDto>> GenerateForgotPasswordToken(string eposta, DateTime gecerlilikSuresi)
        {
            var musteri = await unit.KullaniciRepo.GetByEPostaAsync(eposta);
            if (musteri != null)
            {
                var userData = new UserData();
                userData.Add(SecurityParameters.UserId, musteri.ID.ToString());
                userData.Add(SecurityParameters.EPosta, musteri.Email);
                userData.Add(SecurityParameters.Expires, gecerlilikSuresi.ToString());
                userData.Add(SecurityParameters.UserType, SecurityParameters.PersonelType);

                string seriUserData = JsonConvert.SerializeObject(userData);
                var token = EnDeCode.Encrypt(seriUserData, SecurityParameters.SifrelemeParametresi);

                return new UserLoginResDto<KullaniciListDto>
                {
                    User = mapper.Map<KullaniciListDto>(musteri),
                    Token = token
                };

            }
            else
            {
                throw new Exception("E-Posta Hatalı..");
            }
        }

        public async Task<KullaniciListDto> UpdatePassword(int id, SifremiUnuttumDto model)
        {
            var Entity = await unit.KullaniciRepo.GetByIdAsync(id);

            try
            {
                if (model.YeniParola != model.YeniParolaTekrar)
                {
                    throw new InvalidOperationException($"{Entity.Ad} adlı kullanıcı için girilen şifre eski şifre ile aynı değil. Ekleme başarısız oldu...");
                }
                else
                {
                    var sifre = EnDeCode.Encrypt(model.YeniParola, SecurityParameters.SifrelemeParametresi);
                    Entity.Parola = sifre;
                    await unit.KullaniciRepo.UpdateAsync(Entity);
                    await unit.SaveAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mapper.Map<KullaniciListDto>(Entity);
        }
    }
}
