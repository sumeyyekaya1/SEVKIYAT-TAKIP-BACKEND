using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Application.OrderTypeEnums;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlobalTradeSevkiyatTakip.Persistance.Services
{
    public class CariServis : ICariServis
    {
        private readonly IUOW unit;
        private readonly IMapper mapper;
        public CariServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task<CariListDto> AddAsync(CariInsertDto dto)
        {
            try
            {
                var entity = mapper.Map<Cari>(dto);
                entity = await unit.CariRepo.AddAsync(entity);
                await unit.SaveAsync();
                return mapper.Map<CariListDto>(entity);
            }
            catch (Exception)
            { throw; }
        }

        public async Task<List<EntegreResDto>> AddWolvoxAsync(List<CariEntegreDto> dto)
        {
            List<EntegreResDto> cariler = new List<EntegreResDto>();
            var transaction = unit.BeginTransaction();
            try
            {
                foreach (var item in dto)
                {
                    Cari cari = new Cari();
                    cari.CariKodu = item.CariKodu;
                    cari.WolvoxBlKodu = item.WolvoxBlKodu;
                    cari.Email = item.Email;
                    cari.Ad = item.Adi;
                    cari.Soyad = item.Soyadi;
                    cari.TcNo = item.TcNo;
                    cari.TicariUnvan = item.TicariUnvan;
                    cari.VergiDairesi = item.VergiDairesi;
                    cari.VergiNo = item.VergiNo;
                    cari.Iletisim = item.Iletisim;
                    var sendedCari = await unit.CariRepo.AddCariRawQuery(cari);
                    var returnedValue = new EntegreResDto { ID = sendedCari.ID, WolvoxID = (long)sendedCari.WolvoxBlKodu };
                    cariler.Add(returnedValue);
                }
                await transaction.CommitAsync();
                return cariler;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Cari Eklerken Hata Oluştu." + "Cari Eklerken Hata Oluştu.");
            }
        }

        public async Task<PagedApiResponse<List<CariListDto>>> GetPagedListAsync(PagedRequestParam pagedParam)
        {
            try
            {
                var queryable = unit.CariRepo.GetAllAsync();
                queryable = string.IsNullOrWhiteSpace(pagedParam.SearchText) ? queryable :
                        queryable.Where
                        (
                            x =>
                            x.CariKodu.ToLower().Contains(pagedParam.SearchText) || x.TicariUnvan.ToLower().Contains(pagedParam.SearchText)
                        //varsa eşleştirilecek diğer alanlar eklenebilir...
                        );


                switch ((ListOrderTypes)pagedParam.OrderType)
                {
                    case ListOrderTypes.AdaGoreArtan: queryable = queryable.OrderBy(x => x.TicariUnvan); break;
                    case ListOrderTypes.AdaGoreAzalan: queryable = queryable.OrderByDescending(x => x.TicariUnvan); break;

                }


                int itemCount = queryable.Count();
                var entityList = await queryable.Skip(pagedParam.PerPageItemCount * (pagedParam.CurrentPage - 1))
                                                .Take(pagedParam.PerPageItemCount).ToListAsync();


                var response = new PagedApiResponse<List<CariListDto>>(true, mapper.Map<List<CariListDto>>(entityList))
                {
                    CurrentPage = pagedParam.CurrentPage,
                    OrderType = pagedParam.OrderType,
                    PerPageItemCount = pagedParam.PerPageItemCount,
                    SearchText = pagedParam.SearchText,
                    PageCount = (int)Math.Ceiling(Convert.ToDecimal(itemCount) / Convert.ToDecimal(pagedParam.PerPageItemCount)),
                    Message = "Cariler listelendi"
                };


                return response;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<List<CariListDto>> GetAllAsync()
        {
            try
            {
                var entity = await unit.CariRepo.GetAllAsync().ToListAsync();
                return mapper.Map<List<CariListDto>>(entity);
            }
            catch (Exception)
            { throw; }
        }
        public async Task<int> ToplamCariAdet()
        {
            try
            {
                var entity = await unit.CariRepo.GetAllAsync().ToListAsync();
                return entity.Count();
            }
            catch (Exception)
            { throw; }
        }

        public async Task<CariListDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await unit.CariRepo.GetByIdAsync(id);
                return mapper.Map<CariListDto>(entity);
            }
            catch (Exception)
            { throw; }
        }
        public async Task<CariListDto> RemoveAsync(CariInsertDto dto)
        {
            try
            {
                var Entity = mapper.Map<Cari>(dto);
                Entity = await unit.CariRepo.RemoveAsync(Entity);
                await unit.SaveAsync();
                return mapper.Map<CariListDto>(Entity);
            }
            catch (Exception)
            { throw; }
        }

        public async Task<CariListDto> RemoveAsync(int id)
        {
            try
            {
                var Entity = await unit.CariRepo.RemoveAsync(id);
                await unit.SaveAsync();
                return mapper.Map<CariListDto>(Entity);
            }
            catch (Exception)
            { throw; }
        }

        public async Task<CariListDto> UpdateAsync(CariInsertDto dto)
        {
            try
            {
                var Entity = mapper.Map<Cari>(dto);
                Entity = await unit.CariRepo.UpdateAsync(Entity);
                await unit.SaveAsync();
                return mapper.Map<CariListDto>(Entity);
            }
            catch (Exception)
            { throw; }
        }
    }
}
