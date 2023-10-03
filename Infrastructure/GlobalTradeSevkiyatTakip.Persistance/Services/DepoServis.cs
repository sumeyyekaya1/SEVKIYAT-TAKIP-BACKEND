using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.DepoDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.RaporDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Application.OrderTypeEnums;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.NETCore;

namespace GlobalTradeSevkiyatTakip.Persistance.Services
{
    public class DepoServis : IDepoServis
    {
        private readonly IUOW unit;
        private readonly IMapper mapper;
        public DepoServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        public async Task<DepoListDto> AddAsync(DepoInsertDto dto)
        {
            try
            {
                var entity = mapper.Map<Depo>(dto);
                entity = await unit.DepoRepo.AddAsync(entity);
                await unit.SaveAsync();
                return mapper.Map<DepoListDto>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PagedApiResponse<List<DepoListDto>>> GetPagedListAsync(PagedRequestParam pagedParam)
        {
            try
            {
                var queryable = unit.DepoRepo.GetAllAsync().AsQueryable();
                queryable = string.IsNullOrWhiteSpace(pagedParam.SearchText) ? queryable :
                        queryable.Where
                        (
                            x =>
                            x.DepoAd.ToLower().Contains(pagedParam.SearchText)
                        //varsa eşleştirilecek diğer alanlar eklenebilir...
                        );


                switch ((ListOrderTypes)pagedParam.OrderType)
                {
                    case ListOrderTypes.AdaGoreArtan: queryable = queryable.OrderBy(x => x.DepoAd); break;
                    case ListOrderTypes.AdaGoreAzalan: queryable = queryable.OrderByDescending(x => x.DepoAd); break;

                }


                int itemCount = queryable.Count();
                var entityList = await queryable.Skip(pagedParam.PerPageItemCount * (pagedParam.CurrentPage - 1))
                                                .Take(pagedParam.PerPageItemCount).ToListAsync();


                var response = new PagedApiResponse<List<DepoListDto>>(true, mapper.Map<List<DepoListDto>>(entityList))
                {
                    CurrentPage = pagedParam.CurrentPage,
                    OrderType = pagedParam.OrderType,
                    PerPageItemCount = pagedParam.PerPageItemCount,
                    SearchText = pagedParam.SearchText,
                    PageCount = (int)Math.Ceiling(Convert.ToDecimal(itemCount) / Convert.ToDecimal(pagedParam.PerPageItemCount)),
                    Message = "depolar listelendi"
                };


                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<DepoListDto>> GetAllAsync()
        {
            try
            {
                var entity = await unit.DepoRepo.GetAllAsync().ToListAsync();
                return mapper.Map<List<DepoListDto>>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DepoListDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await unit.DepoRepo.GetByIdAsync(id);
                return mapper.Map<DepoListDto>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
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
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<DepoRaporDto>> GetAllReportAsync(int depoId)
        {
            List<DepoRaporDto> depo = new List<DepoRaporDto>();
            try
            {
                var depoDetay = await unit.DepoDetayRepo.GetAllAsync().Include(x=>x.Cari).Include(x=>x.Stok).Include(x=>x.Depo).AsNoTracking().Where(x => x.DepoId == depoId).ToListAsync();
                foreach (var item in depoDetay)
                {
                    DepoRaporDto rapor = new DepoRaporDto();
                    rapor.TicariUnvan = item.Cari.TicariUnvan;
                    rapor.StokAdi = item.Stok.StokAdi;
                    rapor.Adet = (int)item.Adet;
                    rapor.GirisTarih = item.OlusturmaTarih;
                    rapor.DepoAd = item.Depo.DepoAd;
                    rapor.DepoAdres = item.Depo.DepoAdres;
                    depo.Add(rapor);
                }
                return depo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    
    }
}
