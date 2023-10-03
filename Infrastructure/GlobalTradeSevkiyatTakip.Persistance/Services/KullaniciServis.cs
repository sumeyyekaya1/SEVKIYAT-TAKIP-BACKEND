using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.AuthDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.KullaniciDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Application.OrderTypeEnums;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using GlobalTradeSevkiyatTakip.Application.Utilities.Security;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GlobalTradeSevkiyatTakip.Persistance.Services
{
    public class KullaniciServis : IKullaniciServis
    {
        private readonly IUOW unit;
        private readonly IMapper mapper;
        public KullaniciServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        public async Task<KullaniciListDto> AddAsync(KullaniciInsertDto dto)
        {
            try
            {
                var entity = mapper.Map<Kullanici>(dto);
                var sifre = EnDeCode.Encrypt(entity.Parola, SecurityParameters.SifrelemeParametresi);
                entity.Parola = sifre;
                entity.Rol = Domain.Enums.KullaniciRolEnum.Admin;
                entity = await unit.KullaniciRepo.AddAsync(entity);
                await unit.SaveAsync();
                return mapper.Map<KullaniciListDto>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<KullaniciListDto>> GetAllAsync()
        {
            try
            {
                var entity = await unit.KullaniciRepo.GetAllAsync().ToListAsync();
                return mapper.Map<List<KullaniciListDto>>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<KullaniciListDto> GetByIdAsync(int id)
        {

            try
            {
                var entity = await unit.KullaniciRepo.GetByIdAsync(id);
                return mapper.Map<KullaniciListDto>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PagedApiResponse<List<KullaniciListDto>>> GetPagedListAsync(PagedRequestParam pagedParam)
        {
            try
            {
                var queryable = unit.KullaniciRepo.GetAllAsync().AsQueryable();
                queryable = string.IsNullOrWhiteSpace(pagedParam.SearchText) ? queryable :
                        queryable.Where
                        (
                            x =>
                            x.Ad.ToLower().Contains(pagedParam.SearchText)
                        //varsa eşleştirilecek diğer alanlar eklenebilir...
                        );


                switch ((ListOrderTypes)pagedParam.OrderType)
                {
                    case ListOrderTypes.AdaGoreArtan: queryable = queryable.OrderBy(x => x.Ad); break;
                    case ListOrderTypes.AdaGoreAzalan: queryable = queryable.OrderByDescending(x => x.Ad); break;

                }


                int itemCount = queryable.Count();
                var entityList = await queryable.Skip(pagedParam.PerPageItemCount * (pagedParam.CurrentPage - 1))
                                                .Take(pagedParam.PerPageItemCount).ToListAsync();


                var response = new PagedApiResponse<List<KullaniciListDto>>(true, mapper.Map<List<KullaniciListDto>>(entityList))
                {
                    CurrentPage = pagedParam.CurrentPage,
                    OrderType = pagedParam.OrderType,
                    PerPageItemCount = pagedParam.PerPageItemCount,
                    SearchText = pagedParam.SearchText,
                    PageCount = (int)Math.Ceiling(Convert.ToDecimal(itemCount) / Convert.ToDecimal(pagedParam.PerPageItemCount)),
                    Message = "kullanıcılar listelendi"
                };


                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<KullaniciListDto> RemoveAsync(KullaniciInsertDto dto)
        {
            try
            {
                var Entity = mapper.Map<Kullanici>(dto);
                Entity = await unit.KullaniciRepo.RemoveAsync(Entity);
                await unit.SaveAsync();
                return mapper.Map<KullaniciListDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<KullaniciListDto> RemoveAsync(int id)
        {
            try
            {
                var Entity = await unit.KullaniciRepo.RemoveAsync(id);
                await unit.SaveAsync();
                return mapper.Map<KullaniciListDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<KullaniciListDto> UpdateAsync(KullaniciUpdateDto dto)
        {
            try
            {
                var Entity = await unit.KullaniciRepo.GetByIdAsync(dto.ID);
                Entity.Ad = dto.Ad;
                Entity.Soyad=dto.Soyad;
                Entity.Email = dto.Email;
                Entity.Rol = dto.Rol;
                Entity = await unit.KullaniciRepo.UpdateAsync(Entity);
                await unit.SaveAsync();
                return mapper.Map<KullaniciListDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<KullaniciListDto> UpdatePassword(int id, KullaniciParolaUpdateDto model)
        {
            var Entity = await unit.KullaniciRepo.GetByIdAsync(id);
            try
            {
                var sfr = EnDeCode.Decrypt(Entity.Parola, SecurityParameters.SifrelemeParametresi);
                if (model.EskiParola != sfr)
                {
                    throw new InvalidOperationException($"{Entity.AdSoyad} adlı personel için girilen şifre eski şifre ile aynı değil. Ekleme başarısız oldu...");
                }
                else
                {
                    var sifre = EnDeCode.Encrypt(model.YeniParola, SecurityParameters.SifrelemeParametresi);
                    Entity.Parola = sifre;
                    await unit.KullaniciRepo.UpdateAsync(Entity);
                    await unit.SaveAsync();
                }
                return mapper.Map<KullaniciListDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<UserLoginResDto<KullaniciListDto>> LoginAsyn(UserLoginReqDto dto)
        {
            try
            {
                var personel = await unit.KullaniciRepo.GetByEPostaAsync(dto.Email);
                if (personel != null)
                {
                    var sfr = EnDeCode.Decrypt(personel.Parola, SecurityParameters.SifrelemeParametresi);


                    if (sfr == dto.Parola)
                    {
                        var userData = new UserData();
                        userData.Add(SecurityParameters.UserId, personel.ID.ToString());
                        userData.Add(SecurityParameters.UserFullName, personel.AdSoyad);
                        userData.Add(SecurityParameters.EPosta, personel.Email);
                        userData.Add(SecurityParameters.Expires, (dto.BeniHatirla) ? DateTime.Now.AddYears(1).ToString() : DateTime.Now.AddDays(1).ToString());
                        userData.Add(SecurityParameters.LoginDate, DateTime.Now.ToString());
                        userData.Add(SecurityParameters.Rol, ((int)personel.Rol).ToString());
                        userData.Add(SecurityParameters.UserType, SecurityParameters.PersonelType);


                        string seriUserData = JsonConvert.SerializeObject(userData);
                        var token = EnDeCode.Encrypt(seriUserData, SecurityParameters.SifrelemeParametresi);

                        return new UserLoginResDto<KullaniciListDto>
                        {
                            User = mapper.Map<KullaniciListDto>(personel),
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
    }
}
