using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.RaporDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.StokDetayDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Application.OrderTypeEnums;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.NETCore;

namespace GlobalTradeSevkiyatTakip.Persistance.Services
{
    public class StokDetayServis : IStokDetayServis
    {
        private readonly IUOW unit;
        private readonly IMapper mapper;
        public StokDetayServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        public async Task<List<EntegreResDto>> AddWolvoxAsync(List<StokDetayEntegreDto> dto) // STOK GİRİŞ ÇIKIŞ HAREKETİNDE MİKTAR KONTROL EDİLECEK DOĞRU MU DİYE
        {
            List<EntegreResDto> stokDetaylar = new List<EntegreResDto>();
            try
            {
                foreach (var item in dto) //çikolata , şeker
                {
                    var detay = new StokDetay();
                    var cari = await unit.CariRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.CariKodu == item.CariKodu);
                    var depo = await unit.DepoRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.DepoAd == item.DepoAdi);
                    if (depo == null)
                    {
                        var yeniDepo = new Depo();
                        yeniDepo.DepoAd = item.DepoAdi;
                        await unit.DepoRepo.AddAsync(yeniDepo);
                        await unit.SaveAsync();
                        detay.DepoId = yeniDepo.ID;
                    }
                    else
                    {
                        detay.DepoId = depo.ID;
                    }
                    var stok = await unit.StokRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.WolvoxBlKodu == item.WolvoxStokBlKodu);
                    if (cari != null)
                    {
                        detay.CariId = cari.ID;
                    }
                    if (stok != null)
                    {
                        detay.StokId = stok.ID;
                    }

                    detay.WolvoxBlKodu = item.WolvoxBlKodu;

                    detay.GirisMiktar = 0;
                    detay.CikisMiktar = 0;
                    detay.NetMiktar = 0;
                    if (item.TutarTuru == true)
                    {
                        detay.GirisMiktar = item.Miktar;
                    }
                    else
                    {
                        detay.CikisMiktar = item.Miktar;
                    }
                    detay.NetMiktar = detay.GirisMiktar - detay.CikisMiktar;
                    var returnedData = await unit.StokDetayRepo.AddStokDetayRawQuery(detay);
                    await unit.SaveAsync();
                    stokDetaylar.Add(new EntegreResDto { ID = returnedData.ID, WolvoxID = (long)item.WolvoxBlKodu });
                }
                return stokDetaylar;
            }
            catch (Exception ex)
            {
                throw new Exception("Stok Hareket Eklerken Bir Hata Oluştu." + "Stok Hareket Eklerken Bir Hata Oluştu.");
            }
        }
        public async Task<List<StokDetayListDto>> GetAllAsync()
        {
            try
            {
                var entity = await unit.StokDetayRepo.GetAllAsync().ToListAsync();
                return mapper.Map<List<StokDetayListDto>>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<decimal> ToplamStokAdet()
        {
            try
            {
                var entity = await unit.StokDetayRepo.GetAllAsync().ToListAsync();
                decimal toplam = 0;
                foreach (var item in entity)
                {
                    toplam += (decimal)item.NetMiktar;
                }
                return toplam;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<StokDetayListDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await unit.StokDetayRepo.GetByIdAsync(id);
                return mapper.Map<StokDetayListDto>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PagedApiResponse<List<StokDetayListDto>>> GetPagedListAsync(PagedRequestParam pagedParam, int depoId)
        {
            try
            {
                var queryable = unit.StokDetayRepo.GetAllAsync().Include(x => x.Depo).Include(x => x.Cari).Include(x => x.Stok).AsQueryable().Where(x => x.DepoId == depoId);
                queryable = string.IsNullOrWhiteSpace(pagedParam.SearchText) ? queryable :
                        queryable.Where
                        (
                            x =>
                            x.Depo.DepoAd.ToLower().Contains(pagedParam.SearchText) || x.Stok.StokKod.ToLower().Contains(pagedParam.SearchText)
                            || x.Cari.CariKodu.ToLower().Contains(pagedParam.SearchText) || x.Cari.TicariUnvan.ToLower().Contains(pagedParam.SearchText)
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


                var response = new PagedApiResponse<List<StokDetayListDto>>(true, mapper.Map<List<StokDetayListDto>>(entityList))
                {
                    CurrentPage = pagedParam.CurrentPage,
                    OrderType = pagedParam.OrderType,
                    PerPageItemCount = pagedParam.PerPageItemCount,
                    SearchText = pagedParam.SearchText,
                    PageCount = (int)Math.Ceiling(Convert.ToDecimal(itemCount) / Convert.ToDecimal(pagedParam.PerPageItemCount)),
                    Message = "stok detaylar listelendi"
                };


                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<byte[]> CreateReport(List<DepoRaporDto> itemList)
        {
            try
            {
                var deneme = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
                var report = new LocalReport();
                report.DisplayName = "Depo Raporu";
                report.ReportEmbeddedResource = "GlobalTradeSevkiyatTakip.Persistance.Reporting.ReportFiles.DepoRapor.rdlc";
                report.DataSources.Add(new ReportDataSource("DataSet1", itemList));
                byte[] pdfByteArray = report.Render("PDF");

                return pdfByteArray;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<DepoRaporDto>> GetAllReportAsync(int depoId)
        {
            List<DepoRaporDto> depo = new List<DepoRaporDto>();
            try
            {
                var stokDetay = await unit.StokDetayRepo.GetAllAsync().Include(x => x.Cari).Include(x => x.Stok).Include(x => x.Depo).AsNoTracking().Where(x => x.DepoId == depoId).ToListAsync();
                foreach (var item in stokDetay)
                {
                    DepoRaporDto rapor = new DepoRaporDto();
                    rapor.TicariUnvan = item?.Cari.TicariUnvan;
                    rapor.StokAdi = item?.Stok.StokAdi;
                    rapor.DepoAd = item?.Depo.DepoAd;
                    rapor.Adet = (int)item?.NetMiktar;
                    depo.Add(rapor);
                }
                return depo;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
