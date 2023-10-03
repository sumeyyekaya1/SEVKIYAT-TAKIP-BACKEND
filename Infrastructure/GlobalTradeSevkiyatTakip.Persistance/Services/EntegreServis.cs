using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.AuthDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Application.Utilities.Security;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Persistance.Services
{
    public class EntegreServis : IEntegreServis
    {
        private readonly IUOW unit;
        private readonly IMapper mapper;
        public EntegreServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        public async Task<EntegreKullaniciResDto> AddAsync(EntegreKullaniciReqDto dto)
        {
            try
            {
                var Entity = mapper.Map<Entegre>(dto);
                Entity = await unit.EntegreKullaniciRepo.AddAsync(Entity);
                await unit.SaveAsync();
                return mapper.Map<EntegreKullaniciResDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<EntegreKullaniciResDto>> GetAllAsync()
        {
            try
            {
                var EntityList = unit.EntegreKullaniciRepo.GetAllAsync();
                return mapper.Map<List<EntegreKullaniciResDto>>(EntityList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserLoginResDto<EntegreKullaniciResDto>> LoginAsyn(EntegreLoginDto dto)
        {
            try
            {
                var EntegreKullanici = await unit.EntegreKullaniciRepo.GetByEPostaAsync(dto.Email);

                if (EntegreKullanici != null)
                {
                    var sfr = EnDeCode.Decrypt(EntegreKullanici.Parola, SecurityParameters.SifrelemeParametresi);


                    if (sfr == dto.Parola)
                    {
                        var userData = new UserData();
                        userData.Add(SecurityParameters.UserId, EntegreKullanici.ID.ToString());
                        userData.Add(SecurityParameters.EPosta, EntegreKullanici.Email);
                        userData.Add(SecurityParameters.Expires, (dto.BeniHatirla) ? DateTime.Now.AddYears(1).ToString() : DateTime.Now.AddDays(1).ToString());
                        userData.Add(SecurityParameters.LoginDate, DateTime.Now.ToString());
                        userData.Add(SecurityParameters.UserType, SecurityParameters.EntegreType);
                        userData.Add(SecurityParameters.UserFullName, "TTeknoloji Entegratör");


                        string seriUserData = JsonConvert.SerializeObject(userData);
                        var token = EnDeCode.Encrypt(seriUserData, SecurityParameters.SifrelemeParametresi);

                        return new UserLoginResDto<EntegreKullaniciResDto>
                        {
                            User = mapper.Map<EntegreKullaniciResDto>(EntegreKullanici),
                            Token = token
                        };
                    }
                    else
                    {
                        throw new Exception("Kullanıcı adı ya da parola hatalı ");
                    }
                }
                else
                {
                    throw new Exception("Kullanıcı adı ya da parola hatalı ");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<EntegreKullaniciResDto> RemoveAsync(EntegreKullaniciReqDto dto)
        {
            try
            {
                var Entity = mapper.Map<Entegre>(dto);
                Entity = await unit.EntegreKullaniciRepo.RemoveAsync(Entity);
                await unit.SaveAsync();
                return mapper.Map<EntegreKullaniciResDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EntegreKullaniciResDto> RemoveAsync(int id)
        {
            try
            {
                var Entity = await unit.EntegreKullaniciRepo.RemoveAsync(id);
                await unit.SaveAsync();
                return mapper.Map<EntegreKullaniciResDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserLoginResDto<EntegreKullaniciResDto>> TokenExpiredControl(string token)
        {
            try
            {
                if (token.Contains("Token") && token.StartsWith("Token"))
                {
                    token = token.Substring("Token".Length).Trim();
                    string seriUserData = EnDeCode.Decrypt(token, SecurityParameters.SifrelemeParametresi);

                    UserData userData = JsonConvert.DeserializeObject<UserData>(seriUserData);
                    if (userData != null && Convert.ToDateTime(userData[SecurityParameters.Expires]) > DateTime.Now)
                    {
                        var EntegreKullanici = await unit.EntegreKullaniciRepo.GetByEPostaAsync(userData[SecurityParameters.EPosta]);
                        return new UserLoginResDto<EntegreKullaniciResDto>
                        {
                            User = mapper.Map<EntegreKullaniciResDto>(EntegreKullanici),
                            Token = token
                        };
                    }
                }
                throw new Exception("Token Geçerli Değil..");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EntegreKullaniciResDto> UpdateAsync(EntegreKullaniciReqDto dto)
        {
            try
            {
                var Entity = mapper.Map<Entegre>(dto);
                Entity = await unit.EntegreKullaniciRepo.UpdateAsync(Entity);
                await unit.SaveAsync();
                return mapper.Map<EntegreKullaniciResDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
