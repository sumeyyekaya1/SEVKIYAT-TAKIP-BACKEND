using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.HizmetDTOs;
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
    public class HizmetServis : IHizmetServis
    {
        private readonly IUOW unit;
        private readonly IMapper mapper;
        public HizmetServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public Task<HizmetListDto> AddAsync(HizmetInsertDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EntegreResDto>> AddWolvoxAsync(List<HizmetEntegreDto> dto)
        {
            List<EntegreResDto> hizmetler = new List<EntegreResDto>();
            var transaction = unit.BeginTransaction();
            try
            {
                foreach (var item in dto)
                {
                    Hizmet hizmet = new Hizmet();
                    hizmet.WolvoxBlKodu = item.WolvoxBlKodu;
                    hizmet.HizmetAdi = item.HizmetAdi;
                    var sendedHizmet = await unit.HizmetRepo.AddHizmetRawQuery(hizmet);
                    var returnedValue = new EntegreResDto { ID = sendedHizmet.ID, WolvoxID = (long)sendedHizmet.WolvoxBlKodu };
                    hizmetler.Add(returnedValue);
                }
                await transaction.CommitAsync();
                return hizmetler;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Hizmet Eklerken Hata Oluştu." + "Hizmet Eklerken Hata Oluştu.");
            }
        }

        public Task<List<HizmetListDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<HizmetListDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedApiResponse<List<HizmetListDto>>> GetPagedListAsync(PagedRequestParam pagedParam)
        {
            try
            {
                var queryable = unit.HizmetRepo.GetAllAsync();
                queryable = string.IsNullOrWhiteSpace(pagedParam.SearchText) ? queryable :
                        queryable.Where
                        (
                            x =>
                            x.HizmetAdi.ToLower().Contains(pagedParam.SearchText)
                        //varsa eşleştirilecek diğer alanlar eklenebilir...
                        );


                switch ((ListOrderTypes)pagedParam.OrderType)
                {
                    case ListOrderTypes.AdaGoreArtan: queryable = queryable.OrderBy(x => x.HizmetAdi); break;
                    case ListOrderTypes.AdaGoreAzalan: queryable = queryable.OrderByDescending(x => x.HizmetAdi); break;

                }


                int itemCount = queryable.Count();
                var entityList = await queryable.Skip(pagedParam.PerPageItemCount * (pagedParam.CurrentPage - 1))
                                                .Take(pagedParam.PerPageItemCount).ToListAsync();


                var response = new PagedApiResponse<List<HizmetListDto>>(true, mapper.Map<List<HizmetListDto>>(entityList))
                {
                    CurrentPage = pagedParam.CurrentPage,
                    OrderType = pagedParam.OrderType,
                    PerPageItemCount = pagedParam.PerPageItemCount,
                    SearchText = pagedParam.SearchText,
                    PageCount = (int)Math.Ceiling(Convert.ToDecimal(itemCount) / Convert.ToDecimal(pagedParam.PerPageItemCount)),
                    Message = "Hizmetler listelendi"
                };


                return response;
            }
            catch (Exception)
            { throw; }
        }

        public Task<HizmetListDto> RemoveAsync(HizmetInsertDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<HizmetListDto> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<HizmetListDto> UpdateAsync(HizmetInsertDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
