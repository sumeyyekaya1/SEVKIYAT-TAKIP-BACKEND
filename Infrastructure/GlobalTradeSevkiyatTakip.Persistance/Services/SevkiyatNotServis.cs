using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.SevkiyatNotDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Application.OrderTypeEnums;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlobalTradeSevkiyatTakip.Persistance.Services
{
    public class SevkiyatNotServis : ISevkiyatNotServis
    {
        private readonly IUOW unit;
        private readonly IMapper mapper;
        public SevkiyatNotServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task<SevkiyatNotInsertResDto> AddAsync(SevkiyatNotInsertReqDto dto,int kullaniciId)
        {
            try
            {
                var entity = mapper.Map<SevkiyatNot>(dto);
                entity.KullaniciId = kullaniciId;
                entity = await unit.SevkiyatNotRepo.AddAsync(entity);
                await unit.SaveAsync();
                return mapper.Map<SevkiyatNotInsertResDto>(entity);
            }
            catch (Exception)
            { throw; }
        }

        public async Task<List<SevkiyatNotInsertResDto>> GetAllAsync()
        {
            try
            {
                var entity = await unit.SevkiyatNotRepo.GetAllAsync().ToListAsync();
                return mapper.Map<List<SevkiyatNotInsertResDto>>(entity);
            }
            catch (Exception)
            { throw; }
        }

        public Task<SevkiyatNotInsertResDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedApiResponse<List<SevkiyatNotInsertResDto>>> GetPagedListAsync(PagedRequestParam pagedParam)
        {
            try
            {
                var queryable = unit.SevkiyatNotRepo.GetAllAsync().Include(x=>x.Kullanici).AsQueryable();
                queryable = string.IsNullOrWhiteSpace(pagedParam.SearchText) ? queryable :
                        queryable.Where
                        (
                            x =>
                            x.Kullanici.Ad.ToLower().Contains(pagedParam.SearchText)
                        //varsa eşleştirilecek diğer alanlar eklenebilir...
                        );


                switch ((ListOrderTypes)pagedParam.OrderType)
                {
                    case ListOrderTypes.AdaGoreArtan: queryable = queryable.OrderBy(x => x.Kullanici.Ad); break;
                    case ListOrderTypes.AdaGoreAzalan: queryable = queryable.OrderByDescending(x => x.Kullanici.Ad); break;

                }


                int itemCount = queryable.Count();
                var entityList = await queryable.Skip(pagedParam.PerPageItemCount * (pagedParam.CurrentPage - 1))
                                                .Take(pagedParam.PerPageItemCount).ToListAsync();


                var response = new PagedApiResponse<List<SevkiyatNotInsertResDto>>(true, mapper.Map<List<SevkiyatNotInsertResDto>>(entityList))
                {
                    CurrentPage = pagedParam.CurrentPage,
                    OrderType = pagedParam.OrderType,
                    PerPageItemCount = pagedParam.PerPageItemCount,
                    SearchText = pagedParam.SearchText,
                    PageCount = (int)Math.Ceiling(Convert.ToDecimal(itemCount) / Convert.ToDecimal(pagedParam.PerPageItemCount)),
                    Message = "sevkiyatlar notları listelendi"
                };


                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Task<SevkiyatNotInsertResDto> RemoveAsync(SevkiyatNotInsertReqDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<SevkiyatNotInsertResDto> RemoveAsync(int id)
        {
            try
            {
                var Entity = await unit.SevkiyatNotRepo.RemoveAsync(id);
                await unit.SaveAsync();
                return mapper.Map<SevkiyatNotInsertResDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SevkiyatNotInsertResDto> UpdateAsync(SevkiyatNotInsertReqDto dto)
        {
            try
            {
                var Entity = mapper.Map<SevkiyatNot>(dto);
                Entity = await unit.SevkiyatNotRepo.UpdateAsync(Entity);
                await unit.SaveAsync();
                return mapper.Map<SevkiyatNotInsertResDto>(Entity);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
