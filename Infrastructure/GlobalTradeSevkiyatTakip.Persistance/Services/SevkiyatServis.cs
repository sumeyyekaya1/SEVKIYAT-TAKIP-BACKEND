using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.RaporDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.SevkiyatDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Application.OrderTypeEnums;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.NETCore;
using Microsoft.ReportingServices.Interfaces;

namespace GlobalTradeSevkiyatTakip.Persistance.Services
{
    public class SevkiyatServis : ISevkiyatServis
    {
        private readonly IUOW unit;
        private readonly IMapper mapper;
        public SevkiyatServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        public async Task<SevkiyatListDto> AddAsync(SevkiyatInsertDto dto)
        {
            try
            {
                var entity = mapper.Map<Sevkiyat>(dto);
                entity.NetMaliyet = 0;
                entity.Zarar = 0;
                entity.Kar = 0;
                entity = await unit.SevkiyatRepo.AddAsync(entity);
                await unit.SaveAsync();
                var fatura = await unit.FaturaRepo.GetAllAsync().AsNoTracking().Where(x => x.ProjeNo == entity.ProjeNo).ToListAsync();
                var irsaliye = await unit.IrsaliyeRepo.GetAllAsync().AsNoTracking().Where(x => x.ProjeNo == entity.ProjeNo).ToListAsync();

                foreach (var item in fatura)
                {
                    var sevkiyatDetay = new SevkiyatDetay();
                    sevkiyatDetay.FaturaId = item.ID;
                    sevkiyatDetay.SevkiyatId = entity.ID;
                    
                    sevkiyatDetay = await unit.SevkiyatDetayRepo.AddAsync(sevkiyatDetay);

                    if (item.FaturaTur == Domain.Enums.FaturaTurEnum.Alis)
                    {
                        entity.Zarar += item.WolvoxDovizBirimTutar;
                    }
                    else
                    {
                        entity.Kar += item.WolvoxDovizBirimTutar;
                    }
                    entity.NetMaliyet = entity.Kar - entity.Zarar;
                    entity = await unit.SevkiyatRepo.UpdateAsync(entity);
                    await unit.SaveAsync();
                }
                foreach (var item in irsaliye)
                {
                    var sevkiyatDetay = new SevkiyatDetay();
                    sevkiyatDetay.IrsaliyeId = item.ID;
                    sevkiyatDetay.SevkiyatId = entity.ID;
                    sevkiyatDetay = await unit.SevkiyatDetayRepo.AddAsync(sevkiyatDetay);
                    item.SevkDurum = Domain.Enums.IrsaliyeSevkDurumEnum.SevkiyataEklendi;
                    await unit.IrsaliyeRepo.UpdateAsync(item);
                    await unit.SaveAsync();
                }

                return mapper.Map<SevkiyatListDto>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PagedApiResponse<List<SevkiyatListDto>>> GetPagedListAsync(PagedRequestParam pagedParam)
        {
            try
            {
                var queryable = unit.SevkiyatRepo.GetAllAsync().AsQueryable();
                queryable = string.IsNullOrWhiteSpace(pagedParam.SearchText) ? queryable :
                        queryable.Where
                        (
                            x =>
                            x.ProjeNo.ToLower().Contains(pagedParam.SearchText)
                        //varsa eşleştirilecek diğer alanlar eklenebilir...
                        );


                switch ((ListOrderTypes)pagedParam.OrderType)
                {
                    case ListOrderTypes.AdaGoreArtan: queryable = queryable.OrderBy(x => x.ProjeNo); break;
                    case ListOrderTypes.AdaGoreAzalan: queryable = queryable.OrderByDescending(x => x.ProjeNo); break;

                }


                int itemCount = queryable.Count();
                var entityList = await queryable.Skip(pagedParam.PerPageItemCount * (pagedParam.CurrentPage - 1))
                                                .Take(pagedParam.PerPageItemCount).ToListAsync();


                var response = new PagedApiResponse<List<SevkiyatListDto>>(true, mapper.Map<List<SevkiyatListDto>>(entityList))
                {
                    CurrentPage = pagedParam.CurrentPage,
                    OrderType = pagedParam.OrderType,
                    PerPageItemCount = pagedParam.PerPageItemCount,
                    SearchText = pagedParam.SearchText,
                    PageCount = (int)Math.Ceiling(Convert.ToDecimal(itemCount) / Convert.ToDecimal(pagedParam.PerPageItemCount)),
                    Message = "sevkiyatlar listelendi"
                };


                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<SevkiyatListDto>> GetAllAsync()
        {
            try
            {
                var entity = await unit.SevkiyatRepo.GetAllAsync().ToListAsync();
                return mapper.Map<List<SevkiyatListDto>>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<SevkiyatKurDonusumDto> DovizBirimDonusum(int sevkiyatId,int paraBirimId)
        {
            try
            {
                SevkiyatKurDonusumDto donusumKur = new SevkiyatKurDonusumDto();
                var sevkiyatDetay = await unit.SevkiyatDetayRepo.GetAllAsync().Include(x => x.Fatura).ThenInclude(x=>x.Doviz).Include(x=>x.Fatura).ThenInclude(x=>x.Detay).AsNoTracking().Where(x => x.SevkiyatId == sevkiyatId && x.FaturaId != null).ToListAsync();
                var paraBirim = await unit.ParaBirimRepo.GetByIdAsync(paraBirimId);
                donusumKur.BirimSimge = paraBirim.BirimSimge;

                foreach (var item in sevkiyatDetay)
                {
                    var dovizKur = await unit.DovizRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.DovizBirim == paraBirim.BirimSimge
                                  && x.Tarih.Value.Month == item.Fatura.OlusturmaTarih.Month && x.Tarih.Value.Day == item.Fatura.Tarih.Value.Day);

                    if(dovizKur != null)
                    {
                        var dovizBirim = await unit.DovizRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.DovizBirim == "$"
                                        && x.Tarih.Value.Month == item.Fatura.OlusturmaTarih.Month && x.Tarih.Value.Day == item.Fatura.Tarih.Value.Day);

                        var turkLirasi = item.Fatura.WolvoxDovizBirimTutar * dovizBirim.SatisFiyat;

                        if (item.Fatura.FaturaTur == Domain.Enums.FaturaTurEnum.Alis)
                        {
                            donusumKur.Zarar += turkLirasi / dovizKur.SatisFiyat;
                            donusumKur.Kar = 0;
                        }
                        else
                        {
                            donusumKur.Kar += turkLirasi / dovizKur.SatisFiyat;
                            donusumKur.Zarar = 0;
                        }
                        donusumKur.NetMaliyet = donusumKur.Kar - donusumKur.Zarar;
                    }
                }
                return mapper.Map<SevkiyatKurDonusumDto>(donusumKur);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<int>> AylikSevkiyatAdet()
        {
            try
            {
                List<int> liste = new List<int>();
                for(var i = 1; i<=12; i++)
                {
                    liste.Add(await unit.SevkiyatRepo.GetAllAsync().AsNoTracking().Where(x => x.OlusturmaTarih.Year > DateTime.Now.Year - 1 && x.OlusturmaTarih.Month == i).CountAsync());
                }
                return liste;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SevkiyatListDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await unit.SevkiyatRepo.GetByIdAsync(id);
                return mapper.Map<SevkiyatListDto>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<SevkiyatListDto> RemoveAsync(SevkiyatInsertDto dto)
        {
            try
            {
                var Entity = mapper.Map<Sevkiyat>(dto);
                Entity = await unit.SevkiyatRepo.RemoveAsync(Entity);
                await unit.SaveAsync();
                return mapper.Map<SevkiyatListDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SevkiyatListDto> RemoveAsync(int id)
        {
            try
            {
                var Entity = await unit.SevkiyatRepo.RemoveAsync(id);
                await unit.SaveAsync();
                return mapper.Map<SevkiyatListDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> ToplamSevkiyatAdet()
        {
            try
            {
                var Entity = await unit.SevkiyatRepo.GetAllAsync().ToListAsync();
                return Entity.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SevkiyatListDto> UpdateAsync(SevkiyatInsertDto dto)
        {
            try
            {
                var Entity = mapper.Map<Sevkiyat>(dto);
                Entity = await unit.SevkiyatRepo.UpdateAsync(Entity);
                await unit.SaveAsync();
                return mapper.Map<SevkiyatListDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //GENEL ANALİZ RAPORU İÇİN METOD YAZILACAK
        public async Task<byte[]> CreateReport(List<SevkiyatCekiListeRaporDto> itemList)
        {
            try
            {
                var deneme = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
                var report = new LocalReport();
                report.DisplayName = "Sevkiyat Raporu";
                report.ReportEmbeddedResource = "GlobalTradeSevkiyatTakip.Persistance.Reporting.ReportFiles.CekiListeRapor.rdlc";
                report.DataSources.Add(new ReportDataSource("DataSet1", itemList));
                byte[] pdfByteArray = report.Render("PDF");

                return pdfByteArray;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
     
        public async Task<List<SevkiyatMaliyetRaporDto>> DonemSevkiyatReportAsync(DateTime baslangicTarih, DateTime bitisTarih)
        {
            List<SevkiyatMaliyetRaporDto> maliyetListe = new List<SevkiyatMaliyetRaporDto>();
            try
            {
                var entityList = await unit.SevkiyatDetayRepo.GetAllAsync().Include(x => x.Sevkiyat)
                   .Include(x => x.Irsaliye).ThenInclude(x => x.Cari).AsNoTracking()
                   .Where(x => x.Sevkiyat.SevkiyatTarih >= baslangicTarih && x.Sevkiyat.SevkiyatTarih <= bitisTarih.AddDays(1).AddMilliseconds(-1)).ToListAsync();

                foreach (var item in entityList)
                {
                    SevkiyatMaliyetRaporDto rapor = new SevkiyatMaliyetRaporDto();

                    var faturaDetay = await unit.FaturaDetayRepo.GetAllAsync().Include(x => x.Hizmet).Include(x => x.Fatura).
                        AsNoTracking().Where(x => x.FaturaId == item.FaturaId && x.HizmetId != null).ToListAsync();

                    rapor.ToplamSevkiyatTutar = 0;
                    rapor.AlisToplamTutar = 0;
                    rapor.SatisToplamTutar = 0;
                    foreach (var item2 in faturaDetay)
                    {
                        if (item2.Fatura.FaturaTur == Domain.Enums.FaturaTurEnum.Alis)
                        {
                            rapor.AlisToplamTutar += item2.ToplamTutar;
                            rapor.HizmetTur = "Alış";
                        }
                        else
                        {
                            rapor.SatisToplamTutar += item2.ToplamTutar;
                            rapor.HizmetTur = "Satış";

                        }
                        rapor.ToplamSevkiyatTutar += rapor.SatisToplamTutar - rapor.AlisToplamTutar;
                        rapor.HizmetAd = item2.Hizmet.HizmetAdi;
                        rapor.Miktar = item2.Miktar;
                        rapor.Tutar = item2.ToplamTutar;
                        maliyetListe.Add(rapor);
                    }

                }
                return maliyetListe;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<SevkiyatCekiListeRaporDto>> GetAllReportAsync(int sevkiyatId)
        {
            List<SevkiyatCekiListeRaporDto> cekiListe = new List<SevkiyatCekiListeRaporDto>();
            try
            {
                var entityList = await unit.SevkiyatDetayRepo.GetAllAsync().Include(x=>x.Sevkiyat).
                                                            Include(x => x.Irsaliye).ThenInclude(x=>x.Cari).AsNoTracking().Where(x => x.SevkiyatId == sevkiyatId).ToListAsync();

                foreach (var item in entityList)
                {
                    

                    var irsaliyeDetay = await unit.IrsaliyeDetayRepo.GetAllAsync().Include(x=>x.Stok).ThenInclude(X=>X.Marka)
                        .Include(x => x.Stok).ThenInclude(X=>X.Renk).AsNoTracking().Where(x => x.IrsaliyeId == item.IrsaliyeId).ToListAsync();

                    foreach (var item2 in irsaliyeDetay)
                    {
                        SevkiyatCekiListeRaporDto rapor = new SevkiyatCekiListeRaporDto();
                        rapor.ProjeNo = item.Sevkiyat.ProjeNo;
                        rapor.UrunAciklama = item2?.Stok?.StokAdi;
                        rapor.MusteriKod = item?.Irsaliye?.Cari?.CariKodu;
                        rapor.TakimDurum = item2?.TakimDurum;
                        rapor.Marka = item2?.Stok?.Marka?.MarkaAdi;
                        rapor.PaketIciAdet = item2?.PaketIciAdet;
                        rapor.Icindekiler = item2?.Icindekiler;
                        rapor.UrunIcerik = item2?.UrunIcerik;
                        rapor.TedarikFirma = item?.Irsaliye?.Cari?.TicariUnvan;
                        rapor.KapAdet = item2?.KapAdet;
                        rapor.PaketSekil = item2?.PaketSekil;
                        rapor.Olculer = item2?.Olculer;
                        rapor.KapNo = item2?.KapNo;
                        rapor.PaketIciAdet = item2?.PaketIciAdet;
                        rapor.ToplamPaketIciAdet = item2?.ToplamPaketIciAdet;
                        rapor.ToplamKapAdet = item2?.ToplamKapAdet;
                        rapor.UrunBurutAgirlik = item2?.UrunBurutAgirlik;
                        rapor.UrunNetAgirlik = item2?.UrunNetAgirlik;
                        cekiListe.Add(rapor);

                    }
                   
                }
                return cekiListe.OrderBy(x=>x.KapNo).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<byte[]> CreateReportForMaliyet(List<SevkiyatMaliyetRaporDto> itemList)
        {
            try
            {
                var deneme = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
                var report = new LocalReport();
                report.DisplayName = "Sevkiyat Raporu";
                report.ReportEmbeddedResource = "GlobalTradeSevkiyatTakip.Persistance.Reporting.ReportFiles.SevkiyatMaliyetRapor.rdlc";
                report.DataSources.Add(new ReportDataSource("DataSet1", itemList));
                byte[] pdfByteArray = report.Render("PDF");

                return pdfByteArray;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<SevkiyatMaliyetRaporDto>> ReportForMaliyetAsync(int sevkiyatId)
        {
            try
            {
                List<SevkiyatMaliyetRaporDto> maliyetListe = new List<SevkiyatMaliyetRaporDto>();
                var entityList = await unit.SevkiyatDetayRepo.GetAllAsync().Include(x => x.Sevkiyat).AsNoTracking().Where(x => x.SevkiyatId == sevkiyatId).ToListAsync();
                
                foreach (var item in entityList)
                {
                    var faturaDetay = await unit.FaturaDetayRepo.GetAllAsync().Include(x => x.Hizmet).Include(x => x.Fatura).AsNoTracking().Where(x => x.FaturaId == item.FaturaId && x.HizmetId != null).ToListAsync();

                    foreach (var item2 in faturaDetay)
                    {
                        SevkiyatMaliyetRaporDto rapor = new SevkiyatMaliyetRaporDto();
                        rapor.ToplamSevkiyatTutar = 0;
                        rapor.AlisToplamTutar = 0;
                        rapor.SatisToplamTutar = 0;
                        
                        if (item2.Fatura.FaturaTur == Domain.Enums.FaturaTurEnum.Alis)
                        {
                            rapor.HizmetTur = "Alış";
                        }
                        else
                        {
                            rapor.HizmetTur = "Satış";
                        }

                        rapor.HizmetAd = item2.Hizmet.HizmetAdi;
                        rapor.ProjeNo = item.Sevkiyat.ProjeNo;
                        rapor.Miktar=item2.Miktar;
                        rapor.Tutar = item2.ToplamTutar;
                        maliyetListe.Add(rapor);
                    }
                }
                return maliyetListe;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
