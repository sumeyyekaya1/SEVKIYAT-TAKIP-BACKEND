using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.IrsaliyeDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Application.OrderTypeEnums;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlobalTradeSevkiyatTakip.Persistance.Services
{
    public class IrsaliyeDetayServis : IIrsaliyeDetayServis
    {
        private readonly IUOW unit;
        private readonly IMapper mapper;
        public IrsaliyeDetayServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        public async Task<IrsaliyeDetayListDto> AddAsync(IrsaliyeDetayInsertDto dto)
        {
            try
            {
                var entity = mapper.Map<IrsaliyeDetay>(dto);
                var stok = await unit.StokRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.ID == dto.StokId);
                if(stok != null)
                {
                    entity.DepoId = stok.DepoId;
                }
                entity = await unit.IrsaliyeDetayRepo.AddAsync(entity);
                await unit.SaveAsync();
                var irsaliyeDetay = await unit.IrsaliyeDetayRepo.GetByIdAsync((int)entity.ID);
                return mapper.Map<IrsaliyeDetayListDto>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<IrsaliyeDetayListDto>> GetAllAsync()
        {
            try
            {
                var entity = await unit.IrsaliyeDetayRepo.GetAllAsync().ToListAsync();
                return mapper.Map<List<IrsaliyeDetayListDto>>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PagedApiResponse<List<IrsaliyeDetayListDto>>> GetPagedListAsync(PagedRequestParam pagedParam, int irsaliyeId)
        {
            try
            {
                var queryable = unit.IrsaliyeDetayRepo.GetAllAsync().Include(x=>x.Irsaliye).ThenInclude(x=>x.Cari).Include(x => x.Stok).Include(x => x.Hizmet).AsQueryable().Where(x => x.IrsaliyeId == irsaliyeId);
                queryable = string.IsNullOrWhiteSpace(pagedParam.SearchText) ? queryable :
                        queryable.Where
                        (
                            x =>
                            x.Stok.StokAdi.ToLower().Contains(pagedParam.SearchText) 
                        //varsa eşleştirilecek diğer alanlar eklenebilir...
                        );


                switch ((ListOrderTypes)pagedParam.OrderType)
                {
                    case ListOrderTypes.AdaGoreArtan: queryable = queryable.OrderBy(x => x.Stok.StokAdi); break;
                    case ListOrderTypes.AdaGoreAzalan: queryable = queryable.OrderByDescending(x => x.Stok.StokAdi); break;

                }


                int itemCount = queryable.Count();
                var entityList = await queryable.Skip(pagedParam.PerPageItemCount * (pagedParam.CurrentPage - 1))
                                                .Take(pagedParam.PerPageItemCount).ToListAsync();


                var response = new PagedApiResponse<List<IrsaliyeDetayListDto>>(true, mapper.Map<List<IrsaliyeDetayListDto>>(entityList))
                {
                    CurrentPage = pagedParam.CurrentPage,
                    OrderType = pagedParam.OrderType,
                    PerPageItemCount = pagedParam.PerPageItemCount,
                    SearchText = pagedParam.SearchText,
                    PageCount = (int)Math.Ceiling(Convert.ToDecimal(itemCount) / Convert.ToDecimal(pagedParam.PerPageItemCount)),
                    Message = "paket detaylar listelendi"
                };


                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IrsaliyeDetayListDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await unit.IrsaliyeDetayRepo.GetByIdAsync(id);
                return mapper.Map<IrsaliyeDetayListDto>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IrsaliyeDetayListDto> RemoveAsync(IrsaliyeDetayInsertDto dto)
        {
            try
            {
                var Entity = mapper.Map<IrsaliyeDetay>(dto);
                Entity = await unit.IrsaliyeDetayRepo.RemoveAsync(Entity);
                await unit.SaveAsync();
                return mapper.Map<IrsaliyeDetayListDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IrsaliyeDetayListDto> RemoveAsync(int id)
        {
            try
            {
                var Entity = await unit.IrsaliyeDetayRepo.RemoveAsync(id);
                await unit.SaveAsync();
                return mapper.Map<IrsaliyeDetayListDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IrsaliyeDetayListDto> UpdateAsync(IrsaliyeDetayInsertDto dto)
        {
            try
            {
                var Entity = mapper.Map<IrsaliyeDetay>(dto);
                Entity = await unit.IrsaliyeDetayRepo.UpdateAsync(Entity);
                await unit.SaveAsync();
                var result = await unit.IrsaliyeDetayRepo.GetByIdAsync(Entity.ID);
                return mapper.Map<IrsaliyeDetayListDto>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
