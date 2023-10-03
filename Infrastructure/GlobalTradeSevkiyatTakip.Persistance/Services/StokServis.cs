using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.StokDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Application.OrderTypeEnums;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlobalTradeSevkiyatTakip.Persistance.Services
{
    public class StokServis : IStokServis
    {
        private readonly IUOW unit;
        private readonly IMapper mapper;
        public StokServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        public async Task<StokListDto> AddAsync(StokInsertDto dto)
        {
            try
            {
                var entity = mapper.Map<Stok>(dto);
                entity = await unit.StokRepo.AddAsync(entity);
                await unit.SaveAsync();
                return mapper.Map<StokListDto>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<EntegreResDto>> AddWolvoxAsync(List<StokEntegreDto> dto)
        {
            List<EntegreResDto> stoklar = new List<EntegreResDto>();
            try
            {
                foreach (var item in dto)
                {
                    var stok = new Stok();
                    stok.Barkod = item.Barkod;
                    stok.StokKod = item.StokKod;
                    stok.StokAdi = item.StokAdi;
                    stok.WolvoxBlKodu = item.WolvoxBlKodu;
                    stok.KdvOran = item.KdvOran;
                    stok.IskontoOran = item.IskontoOran;
                    stok.DovizBirim = item.DovizBirim;
                    stok.RenkId = await AddRenkWolvoxAsync(item.Renk);
                    stok.MarkaId = await AddWolvoxMarkaAsync(item.Marka);
                    stok.DepoId = await AddWolvoxDepoAsync(item.DepoAdi);

                    var returnedData = await unit.StokRepo.AddStokRawQuery(stok);
                    stoklar.Add(new EntegreResDto { ID = returnedData.ID, WolvoxID = (long)returnedData.WolvoxBlKodu });
                }

                return stoklar;
            }
            catch (Exception ex)
            {
                throw new Exception("Stok Eklerken Bir Hata Oluştu." + "Stok Eklerken Bir Hata Oluştu.");
            }

        }

        private async Task<int?> AddRenkWolvoxAsync(string? renkAd)
        {
            using (var transaction = unit.BeginTransaction())
            {
                try
                {
                    if (!string.IsNullOrEmpty(renkAd))
                    {
                        var renk = await unit.RenkRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.RenkAdi == renkAd);
                        if (renk == null)
                        {
                            renk = await unit.RenkRepo.AddAsync(new Renk { RenkAdi=renkAd});
                            await unit.SaveAsync();
                        }
                        await transaction.CommitAsync();
                        return renk.ID;
                    }
                    return null;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                } 
            }
        }

        public async Task<int?> AddWolvoxMarkaAsync(string? markaAd)
        {
            using (var transaction = unit.BeginTransaction())
            {
                try
                {
                    if (!string.IsNullOrEmpty(markaAd))
                    {
                        var marka = await unit.MarkaRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.MarkaAdi == markaAd);
                        if (marka == null)
                        {
                            marka = await unit.MarkaRepo.AddAsync(new Marka { MarkaAdi=markaAd});
                            await unit.SaveAsync();
                        }
                        await transaction.CommitAsync();
                        return marka.ID;
                    }
                    return null;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                } 
            }
        }
        public async Task<int?> AddWolvoxDepoAsync(string? depoAd)
        {
            using (var transaction = unit.BeginTransaction())
            {
                try
                {
                    if (!string.IsNullOrEmpty(depoAd))
                    {
                        var depo = await unit.DepoRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.DepoAd == depoAd);
                        if (depo == null)
                        {
                            depo = await unit.DepoRepo.AddAsync(new Depo { DepoAd = depoAd });
                            await unit.SaveAsync();
                        }
                        await transaction.CommitAsync();
                        return depo.ID;
                    }
                    return null;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<PagedApiResponse<List<StokListDto>>> GetPagedListAsync(PagedRequestParam pagedParam)
        {
            try
            {
                var queryable = unit.StokRepo.GetAllAsync().Include(x=>x.Depo).AsQueryable();
                queryable = string.IsNullOrWhiteSpace(pagedParam.SearchText) ? queryable :
                        queryable.Where
                        (
                            x =>
                            x.StokAdi.ToLower().Contains(pagedParam.SearchText) || x.StokKod.ToLower().Contains(pagedParam.SearchText)
                        //varsa eşleştirilecek diğer alanlar eklenebilir...
                        );


                switch ((ListOrderTypes)pagedParam.OrderType)
                {
                    case ListOrderTypes.AdaGoreArtan: queryable = queryable.OrderBy(x => x.StokAdi); break;
                    case ListOrderTypes.AdaGoreAzalan: queryable = queryable.OrderByDescending(x => x.StokAdi); break;

                }


                int itemCount = queryable.Count();
                var entityList = await queryable.Skip(pagedParam.PerPageItemCount * (pagedParam.CurrentPage - 1))
                                                .Take(pagedParam.PerPageItemCount).ToListAsync();


                var response = new PagedApiResponse<List<StokListDto>>(true, mapper.Map<List<StokListDto>>(entityList))
                {
                    CurrentPage = pagedParam.CurrentPage,
                    OrderType = pagedParam.OrderType,
                    PerPageItemCount = pagedParam.PerPageItemCount,
                    SearchText = pagedParam.SearchText,
                    PageCount = (int)Math.Ceiling(Convert.ToDecimal(itemCount) / Convert.ToDecimal(pagedParam.PerPageItemCount)),
                    Message = "stoklar listelendi"
                };


                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<StokListDto>> GetAllAsync()
        {
            try
            {
                var entity = await unit.StokRepo.GetAllAsync().ToListAsync();
                return mapper.Map<List<StokListDto>>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<StokListDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await unit.StokRepo.GetByIdAsync(id);
                return mapper.Map<StokListDto>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
