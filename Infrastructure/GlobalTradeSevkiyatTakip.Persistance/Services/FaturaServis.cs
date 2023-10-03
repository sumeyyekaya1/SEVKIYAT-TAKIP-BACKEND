using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.FaturaDTOs;
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
    public class FaturaServis : IFaturaServis
    {
        protected readonly IUOW unit;
        private readonly IMapper mapper;
        public FaturaServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        public async Task<List<EntegreResDto>> AddWolvoxAsync(List<FaturaEntegreViewModelDto> dto)
        {
            List<EntegreResDto> faturalar = new List<EntegreResDto>();
            var transaction = unit.BeginTransaction();
            try
            {
                foreach (var item in dto)
                {
                    Fatura fatura = new Fatura();
                    fatura.WolvoxBlKodu = item.Fatura.WolvoxBlKodu;
                    var irsaliye = await unit.IrsaliyeRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.ProjeNo == item.Fatura.OzelKod);
                    if(irsaliye != null)
                    {
                        irsaliye.FaturaDurum = Domain.Enums.IrsaliyeFaturaDurumEnum.Faturalandi;
                        await unit.IrsaliyeRepo.UpdateAsync(irsaliye);
                        await unit.SaveAsync();
                    }
                    var cari = await unit.CariRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.WolvoxBlKodu == item.Fatura.WolvoxCariBlKodu);
                    if(cari != null)
                    {
                        fatura.CariId = cari.ID;
                    }
                  
                    fatura.Tarih = item.Fatura.Tarih;
                    fatura.FaturaNo = item.Fatura.FaturaNo;
                    var doviz = await unit.ParaBirimRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x=>x.BirimSimge == item.Fatura.DovizBirim);
                    if(doviz != null)
                    {
                        fatura.DovizId = doviz.ID;
                    }
                    fatura.ProjeNo = item.Fatura.OzelKod;
                    fatura.WolvoxFaturaTutar = item.Fatura.WolvoxFaturaTutar;
                    fatura.WolvoxDolarTutar = item.Fatura.WolvoxDolarTutar;

                    if(doviz.BirimSimge != "$")
                    {
                        var birim = await unit.DovizRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.DovizBirim == item.Fatura.DovizBirim
                                        && x.Tarih.Value.Month == fatura.OlusturmaTarih.Month && x.Tarih.Value.Day == fatura.Tarih.Value.Day);
                        if(birim != null)
                        {
                            var turkLirasi = item.Fatura.WolvoxFaturaTutar * birim.SatisFiyat;

                            var dovizBirim = await unit.DovizRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.DovizBirim == "$"
                                      && x.Tarih.Value.Month == fatura.OlusturmaTarih.Month && x.Tarih.Value.Day == fatura.Tarih.Value.Day);
                            if(dovizBirim != null)
                            {
                                fatura.WolvoxDovizBirimTutar = turkLirasi / dovizBirim.SatisFiyat;
                             
                            }
                            else
                            {
                                var sonBirim = await unit.DovizRepo.GetAllAsync().AsNoTracking().OrderByDescending(x => x.Tarih).FirstAsync(x => x.DovizBirim == "$");
                                fatura.WolvoxDovizBirimTutar = turkLirasi / sonBirim.SatisFiyat;
                            }
                          
                        }
                        else
                        {
                            var isNotNullbirim = await unit.DovizRepo.GetAllAsync().AsNoTracking().OrderByDescending(x => x.Tarih).FirstAsync(x => x.DovizBirim == item.Fatura.DovizBirim);
                            var turkLirasi = item.Fatura.WolvoxFaturaTutar * isNotNullbirim.SatisFiyat;

                            var dovizBirim = await unit.DovizRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.DovizBirim == "$"
                                      && x.Tarih.Value.Month == fatura.OlusturmaTarih.Month && x.Tarih.Value.Day == fatura.Tarih.Value.Day);
                            if (dovizBirim != null)
                            {
                                fatura.WolvoxDovizBirimTutar = turkLirasi / dovizBirim.SatisFiyat;

                            }
                            else
                            {
                                var sonBirim = await unit.DovizRepo.GetAllAsync().AsNoTracking().OrderByDescending(x => x.Tarih).FirstAsync(x => x.DovizBirim == "$");
                                fatura.WolvoxDovizBirimTutar = turkLirasi / sonBirim.SatisFiyat;
                            }
                        }
                      
                    }
                    else
                    {
                        fatura.WolvoxDovizBirimTutar = item.Fatura.WolvoxFaturaTutar;
                    }
                    if (item.Fatura.FaturaTip == 5)
                    {
                        fatura.FaturaTur = Domain.Enums.FaturaTurEnum.Alis;
                    }
                    if(item.Fatura.FaturaTip == 1)
                    {
                        fatura.FaturaTur = Domain.Enums.FaturaTurEnum.Satis;
                    }
                    var sendedFatura = await unit.FaturaRepo.AddFaturaRawQuery(fatura);
                    var returnedValue = new EntegreResDto { ID = sendedFatura.ID, WolvoxID = (long)sendedFatura.WolvoxBlKodu };
                    faturalar.Add(returnedValue);
                 
                    foreach(var item2 in item.FaturaDetay)
                    {
                        
                        var faturaDetay = new FaturaDetay();
                        faturaDetay.FaturaId = sendedFatura.ID;
                        faturaDetay.WolvoxBlKodu = item2.WolvoxBlKodu;

                        var detayDoviz = await unit.ParaBirimRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.BirimSimge == item2.DovizBirim);
                        faturaDetay.DovizId = detayDoviz.ID;

                        var detaybirim = await unit.DovizRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.DovizBirim == item2.DovizBirim
                                            && x.Tarih.Value.Month == fatura.Tarih.Value.Month && x.Tarih.Value.Day == fatura.Tarih.Value.Day);

                        if(detaybirim != null)
                        {
                             faturaDetay.ToplamTutar = item2.ToplamTutar / detaybirim.SatisFiyat;
                        }
                        else
                        {
                            var birim = await unit.DovizRepo.GetAllAsync().AsNoTracking().OrderByDescending(x => x.Tarih).FirstAsync(x=>x.DovizBirim == item2.DovizBirim);
                            faturaDetay.ToplamTutar = item2.ToplamTutar / birim.SatisFiyat;
                        }
                     

                        var stok = await unit.StokRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x=>x.WolvoxBlKodu == item2.WolvoxStokBlKodu);
                        if(stok != null)
                        {
                            faturaDetay.StokId = stok.ID;
                        }
                        var hizmet = await unit.HizmetRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.WolvoxBlKodu == item2.WolvoxHizmetBlKodu);
                        if(hizmet!= null)
                        {
                            faturaDetay.HizmetId = hizmet.ID;
                        }
                        faturaDetay.Miktar = item2.Miktar;
                        var sendedFaturaDetay = await unit.FaturaDetayRepo.AddFaturaDetayRawQuery(faturaDetay);
                    }

                    var sevkiyat = await unit.SevkiyatRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.ProjeNo == item.Fatura.OzelKod);
                    if(sevkiyat != null)
                    {
                        var sevkiyatDetay = new SevkiyatDetay();
                        sevkiyatDetay.SevkiyatId = sevkiyat.ID;
                        sevkiyatDetay.FaturaId = sendedFatura.ID;
                        if (item.Fatura.FaturaTip == 1)
                        {
                            sevkiyat.Zarar += sendedFatura.WolvoxDovizBirimTutar;
                        }
                        if(item.Fatura.FaturaTip == 5)
                        {
                            sevkiyat.Kar += sendedFatura.WolvoxDovizBirimTutar; ;
                        }
                        sevkiyat.NetMaliyet = sevkiyat.Kar - sevkiyat.Zarar;
                        await unit.SevkiyatRepo.UpdateAsync(sevkiyat);
                        await unit.SevkiyatDetayRepo.AddAsync(sevkiyatDetay);
                        await unit.SaveAsync();
                    }
                }
                await transaction.CommitAsync();
                return faturalar;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Fatura Eklerken Hata Oluştu." + "Fatura Eklerken Hata Oluştu");
            }
        }

        public Task<List<FaturaListDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<FaturaListDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await unit.FaturaRepo.GetByIdAsync(id);
                return mapper.Map<FaturaListDto>(entity);
            }
            catch (Exception)
            { throw; }
        }

        public async Task<PagedApiResponse<List<FaturaListDto>>> GetPagedListAsync(PagedRequestParam pagedParam)
        {
            try
            {
                var queryable = unit.FaturaRepo.GetAllAsync().Include(x=>x.Cari).AsQueryable();
                queryable = string.IsNullOrWhiteSpace(pagedParam.SearchText) ? queryable :
                        queryable.Where
                        (
                            x =>
                            x.FaturaNo.ToLower().Contains(pagedParam.SearchText) || x.Cari.TicariUnvan.ToLower().Contains(pagedParam.SearchText)
                        //varsa eşleştirilecek diğer alanlar eklenebilir...
                        );


                switch ((ListOrderTypes)pagedParam.OrderType)
                {
                    case ListOrderTypes.AdaGoreArtan: queryable = queryable.OrderBy(x => x.FaturaNo); break;
                    case ListOrderTypes.AdaGoreAzalan: queryable = queryable.OrderByDescending(x => x.FaturaNo); break;

                }


                int itemCount = queryable.Count();
                var entityList = await queryable.Skip(pagedParam.PerPageItemCount * (pagedParam.CurrentPage - 1))
                                                .Take(pagedParam.PerPageItemCount).ToListAsync();


                var response = new PagedApiResponse<List<FaturaListDto>>(true, mapper.Map<List<FaturaListDto>>(entityList))
                {
                    CurrentPage = pagedParam.CurrentPage,
                    OrderType = pagedParam.OrderType,
                    PerPageItemCount = pagedParam.PerPageItemCount,
                    SearchText = pagedParam.SearchText,
                    PageCount = (int)Math.Ceiling(Convert.ToDecimal(itemCount) / Convert.ToDecimal(pagedParam.PerPageItemCount)),
                    Message = "Faturalar listelendi"
                };


                return response;
            }
            catch (Exception)
            { throw; }
        }
    }
}
