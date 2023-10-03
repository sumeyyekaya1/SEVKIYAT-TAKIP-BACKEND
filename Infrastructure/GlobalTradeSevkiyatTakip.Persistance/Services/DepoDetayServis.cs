using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.DepoDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Application.OrderTypeEnums;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlobalTradeSevkiyatTakip.Persistance.Services
{
    public class DepoDetayServis : IDepoDetayServis
    {
        private readonly IUOW unit;
        private readonly IMapper mapper;
        public DepoDetayServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task<DepoDetayListDto> AddAsync(DepoDetayInsertDto dto)
        {
            try
            {
                var entity = mapper.Map<DepoDetay>(dto);
                entity = await unit.DepoDetayRepo.AddAsync(entity);
                await unit.SaveAsync();
                return mapper.Map<DepoDetayListDto>(entity);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PagedApiResponse<List<DepoDetayListDto>>> GetPagedListAsync(PagedRequestParam pagedParam, int depoId)
        {
            try
            {
                var queryable = unit.DepoDetayRepo.GetAllAsync().Include(x => x.Stok).Include(x=>x.Cari).AsQueryable().Where(x => x.DepoId == depoId);
                queryable = string.IsNullOrWhiteSpace(pagedParam.SearchText) ? queryable :
                        queryable.Where
                        (
                            x =>
                            x.Depo.DepoAd.ToLower().Contains(pagedParam.SearchText)
                        //varsa eşleştirilecek diğer alanlar eklenebilir...
                        );


                switch ((ListOrderTypes)pagedParam.OrderType)
                {
                    case ListOrderTypes.AdaGoreArtan: queryable = queryable.OrderBy(x => x.Depo.DepoAd); break;
                    case ListOrderTypes.AdaGoreAzalan: queryable = queryable.OrderByDescending(x => x.Depo.DepoAd); break;

                }


                int itemCount = queryable.Count();
                var entityList = await queryable.Skip(pagedParam.PerPageItemCount * (pagedParam.CurrentPage - 1))
                                                .Take(pagedParam.PerPageItemCount).ToListAsync();


                var response = new PagedApiResponse<List<DepoDetayListDto>>(true, mapper.Map<List<DepoDetayListDto>>(entityList))
                {
                    CurrentPage = pagedParam.CurrentPage,
                    OrderType = pagedParam.OrderType,
                    PerPageItemCount = pagedParam.PerPageItemCount,
                    SearchText = pagedParam.SearchText,
                    PageCount = (int)Math.Ceiling(Convert.ToDecimal(itemCount) / Convert.ToDecimal(pagedParam.PerPageItemCount)),
                    Message = "depo detaylar listelendi"
                };


                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<DepoDetayListDto>> GetAllAsync()
        {
            try
            {
                var entity = await unit.DepoDetayRepo.GetAllAsync().ToListAsync();
                return mapper.Map<List<DepoDetayListDto>>(entity);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DepoDetayListDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await unit.DepoDetayRepo.GetByIdAsync(id);
                return mapper.Map<DepoDetayListDto>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
