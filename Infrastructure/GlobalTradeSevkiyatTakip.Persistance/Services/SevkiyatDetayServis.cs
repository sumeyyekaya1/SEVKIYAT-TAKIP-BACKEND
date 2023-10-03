using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.IrsaliyeDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.SevkiyatDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Application.OrderTypeEnums;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Persistance.Services
{
    public class SevkiyatDetayServis : ISevkiyatDetayServis
    {
        private readonly IUOW unit;
        private readonly IMapper mapper;
        public SevkiyatDetayServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        public async Task<SevkiyatDetayListDto> AddAsync(SevkiyatDetayInsertDto dto)
        {
            try
            {
                var entity = mapper.Map<SevkiyatDetay>(dto);
                var sevkiyat = await unit.SevkiyatRepo.GetByIdAsync((int)entity.SevkiyatId);
                var irsaliye = await unit.IrsaliyeRepo.GetAllAsync().AsNoTracking().Where(x => x.ProjeNo == sevkiyat.ProjeNo).ToListAsync();
                
                foreach (var item in irsaliye)
                {
                    entity.IrsaliyeId = item.ID;
                    item.SevkDurum = Domain.Enums.IrsaliyeSevkDurumEnum.SevkiyataEklendi;
                    await unit.IrsaliyeRepo.UpdateAsync(item);
                    entity = await unit.SevkiyatDetayRepo.AddAsync(entity);
                    await unit.SaveAsync();
                }
                entity = await unit.SevkiyatDetayRepo.GetByIdAsync(entity.ID);
                return mapper.Map<SevkiyatDetayListDto>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<PagedApiResponse<List<SevkiyatDetayListDto>>> GetPagedListForFaturaAsync(PagedRequestParam pagedParam, int sevkiyatId)
        {
            try
            {
                var queryable = unit.SevkiyatDetayRepo.GetAllAsync().Include(x => x.Fatura).ThenInclude(x=>x.Detay).Include(x => x.Fatura).ThenInclude(x => x.Cari)
                    .AsQueryable().Where(x => x.SevkiyatId == sevkiyatId && x.FaturaId!= null);
                queryable = string.IsNullOrWhiteSpace(pagedParam.SearchText) ? queryable :
                        queryable.Where
                        (
                            x =>
                            x.Sevkiyat.ProjeNo.ToLower().Contains(pagedParam.SearchText)
                        //varsa eşleştirilecek diğer alanlar eklenebilir...
                        );


                switch ((ListOrderTypes)pagedParam.OrderType)
                {
                    case ListOrderTypes.AdaGoreArtan: queryable = queryable.OrderBy(x => x.Sevkiyat.ProjeNo); break;
                    case ListOrderTypes.AdaGoreAzalan: queryable = queryable.OrderByDescending(x => x.Sevkiyat.ProjeNo); break;

                }


                int itemCount = queryable.Count();
                var entityList = await queryable.Skip(pagedParam.PerPageItemCount * (pagedParam.CurrentPage - 1))
                                                .Take(pagedParam.PerPageItemCount).ToListAsync();


                var response = new PagedApiResponse<List<SevkiyatDetayListDto>>(true, mapper.Map<List<SevkiyatDetayListDto>>(entityList))
                {
                    CurrentPage = pagedParam.CurrentPage,
                    OrderType = pagedParam.OrderType,
                    PerPageItemCount = pagedParam.PerPageItemCount,
                    SearchText = pagedParam.SearchText,
                    PageCount = (int)Math.Ceiling(Convert.ToDecimal(itemCount) / Convert.ToDecimal(pagedParam.PerPageItemCount)),
                    Message = "sevkiyat detaylar listelendi"
                };


                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<PagedApiResponse<List<SevkiyatDetayListDto>>> GetPagedListForIrsaliyeAsync(PagedRequestParam pagedParam, int sevkiyatId)
        {
            try
            {
                var queryable = unit.SevkiyatDetayRepo.GetAllAsync().Include(x => x.Irsaliye).ThenInclude(x => x.IrsaliyeDetay).Include(x => x.Irsaliye).ThenInclude(x => x.Cari)
                    .AsQueryable().Where(x => x.SevkiyatId == sevkiyatId && x.IrsaliyeId != null);
                queryable = string.IsNullOrWhiteSpace(pagedParam.SearchText) ? queryable :
                        queryable.Where
                        (
                            x =>
                            x.Sevkiyat.ProjeNo.ToLower().Contains(pagedParam.SearchText)
                        //varsa eşleştirilecek diğer alanlar eklenebilir...
                        );


                switch ((ListOrderTypes)pagedParam.OrderType)
                {
                    case ListOrderTypes.AdaGoreArtan: queryable = queryable.OrderBy(x => x.Sevkiyat.ProjeNo); break;
                    case ListOrderTypes.AdaGoreAzalan: queryable = queryable.OrderByDescending(x => x.Sevkiyat.ProjeNo); break;

                }


                int itemCount = queryable.Count();
                var entityList = await queryable.Skip(pagedParam.PerPageItemCount * (pagedParam.CurrentPage - 1))
                                                .Take(pagedParam.PerPageItemCount).ToListAsync();


                var response = new PagedApiResponse<List<SevkiyatDetayListDto>>(true, mapper.Map<List<SevkiyatDetayListDto>>(entityList))
                {
                    CurrentPage = pagedParam.CurrentPage,
                    OrderType = pagedParam.OrderType,
                    PerPageItemCount = pagedParam.PerPageItemCount,
                    SearchText = pagedParam.SearchText,
                    PageCount = (int)Math.Ceiling(Convert.ToDecimal(itemCount) / Convert.ToDecimal(pagedParam.PerPageItemCount)),
                    Message = "sevkiyat detaylar listelendi"
                };


                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<SevkiyatDetayListDto>> GetAllAsync()
        {
            try
            {
                var entity = await unit.SevkiyatDetayRepo.GetAllAsync().ToListAsync();
                return mapper.Map<List<SevkiyatDetayListDto>>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SevkiyatDetayListDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await unit.SevkiyatDetayRepo.GetByIdAsync(id);
                return mapper.Map<SevkiyatDetayListDto>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<SevkiyatDetayListDto> RemoveAsync(SevkiyatDetayInsertDto dto)
        {
            try
            {
                var Entity = mapper.Map<Cari>(dto);
                Entity = await unit.CariRepo.RemoveAsync(Entity);
                await unit.SaveAsync();
                return mapper.Map<SevkiyatDetayListDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SevkiyatDetayListDto> RemoveAsync(int id)
        {
            try
            {
                var Entity = await unit.CariRepo.RemoveAsync(id);
                var sevkiyatDetay = await unit.SevkiyatDetayRepo.GetAllAsync().AsNoTracking().Where(x => x.SevkiyatId == Entity.ID).ToListAsync();
                foreach(var item in sevkiyatDetay)
                {
                    await unit.SevkiyatDetayRepo.RemoveAsync(item.ID);
                }
                await unit.SaveAsync();
                return mapper.Map<SevkiyatDetayListDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SevkiyatDetayListDto> UpdateAsync(SevkiyatDetayInsertDto dto)
        {
            try
            {
                var Entity = mapper.Map<Cari>(dto);
                Entity = await unit.CariRepo.UpdateAsync(Entity);
                await unit.SaveAsync();
                return mapper.Map<SevkiyatDetayListDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
