using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.IrsaliyeDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Application.OrderTypeEnums;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using GlobalTradeSevkiyatTakip.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace GlobalTradeSevkiyatTakip.Persistance.Services
{
    public class IrsaliyeServis : IIrsaliyeServis
    {
        private readonly IUOW unit;
        private readonly IMapper mapper;
        public IrsaliyeServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        public async Task<IrsaliyeListViewModelDto> AddAsync(IrsaliyeViewModelDto dto)
        {
            try
            {
                IrsaliyeListViewModelDto returnModel = new IrsaliyeListViewModelDto();
                var entity = mapper.Map<Irsaliye>(dto.Irsaliye);
             
                entity = await unit.IrsaliyeRepo.AddAsync(entity);
                await unit.SaveAsync();
                var irsaliye = await unit.IrsaliyeRepo.GetAllAsync().Include(x=>x.Cari).AsNoTracking().FirstOrDefaultAsync(x=>x.ID == entity.ID);
                returnModel.Irsaliye = mapper.Map<IrsaliyeListDto>(irsaliye);
                foreach(var item in dto.IrsaliyeDetay)
                {
                    var detay = mapper.Map<IrsaliyeDetay>(item);
                    var stok = await unit.StokRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.ID == item.StokId);
                    detay.DepoId = stok?.DepoId;
                    detay.IrsaliyeId = entity.ID;
                    detay = await unit.IrsaliyeDetayRepo.AddAsync(detay);
                    await unit.SaveAsync();
                    returnModel.IrsaliyeDetay.Add(mapper.Map<IrsaliyeDetayInsertDto>(detay));
                }
                var sevkiyat = await unit.SevkiyatRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.ProjeNo == entity.ProjeNo);
                if(sevkiyat != null)
                {
                    var sevkiyatDetay = new SevkiyatDetay();
                    sevkiyatDetay.IrsaliyeId = entity.ID;
                    sevkiyatDetay.SevkiyatId = sevkiyat.ID;
                    sevkiyatDetay = await unit.SevkiyatDetayRepo.AddAsync(sevkiyatDetay);
                    entity.SevkDurum = IrsaliyeSevkDurumEnum.SevkiyataEklendi;
                    entity = await unit.IrsaliyeRepo.UpdateAsync(entity);
                    await unit.SaveAsync();
                }

                return returnModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PagedApiResponse<List<IrsaliyeListViewModelDto>>> GetPagedListAsync(PagedRequestParam pagedParam)
        {
            try
            {
                var queryable = unit.IrsaliyeRepo.GetAllAsync().Include(x=>x.Cari).Include(x => x.IrsaliyeDetay).AsQueryable();
                queryable = string.IsNullOrWhiteSpace(pagedParam.SearchText) ? queryable :
                        queryable.Where
                        (
                            x =>
                            x.IrsaliyeNo.ToLower().Contains(pagedParam.SearchText)
                        //varsa eşleştirilecek diğer alanlar eklenebilir...
                        );


                switch ((ListOrderTypes)pagedParam.OrderType)
                {
                    case ListOrderTypes.AdaGoreArtan: queryable = queryable.OrderBy(x => x.IrsaliyeNo); break;
                    case ListOrderTypes.AdaGoreAzalan: queryable = queryable.OrderByDescending(x => x.IrsaliyeNo); break;

                }


                int itemCount = queryable.Count();
                var entityList = await queryable.Skip(pagedParam.PerPageItemCount * (pagedParam.CurrentPage - 1))
                                                .Take(pagedParam.PerPageItemCount).ToListAsync();

                List<IrsaliyeListViewModelDto> irsaliyeList = new List<IrsaliyeListViewModelDto>();
                foreach (var item in entityList)
                {
                    if (item != null)
                    {
                        var irsaliye = item;
                        irsaliyeList.Add(new IrsaliyeListViewModelDto { Irsaliye = mapper.Map<IrsaliyeListDto>(irsaliye), IrsaliyeDetay = mapper.Map<List<IrsaliyeDetayInsertDto>>(irsaliye.IrsaliyeDetay) });
                    }

                }

                var response = new PagedApiResponse<List<IrsaliyeListViewModelDto>>(true, mapper.Map<List<IrsaliyeListViewModelDto>>(irsaliyeList))
                {
                    CurrentPage = pagedParam.CurrentPage,
                    OrderType = pagedParam.OrderType,
                    PerPageItemCount = pagedParam.PerPageItemCount,
                    SearchText = pagedParam.SearchText,
                    PageCount = (int)Math.Ceiling(Convert.ToDecimal(itemCount) / Convert.ToDecimal(pagedParam.PerPageItemCount)),
                    Message = "irsaliyeler listelendi"
                };

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<PagedApiResponse<List<IrsaliyeListDto>>> GetPagedListFaturalananAsync(PagedRequestParam pagedParam)
        {
            try
            {
                var queryable = unit.IrsaliyeRepo.GetAllAsync().Include(x => x.Cari).AsQueryable().Where(x=>x.FaturaDurum == Domain.Enums.IrsaliyeFaturaDurumEnum.Faturalandi);
                queryable = string.IsNullOrWhiteSpace(pagedParam.SearchText) ? queryable :
                        queryable.Where
                        (
                            x =>
                            x.IrsaliyeNo.ToLower().Contains(pagedParam.SearchText)
                        //varsa eşleştirilecek diğer alanlar eklenebilir...
                        );


                switch ((ListOrderTypes)pagedParam.OrderType)
                {
                    case ListOrderTypes.AdaGoreArtan: queryable = queryable.OrderBy(x => x.IrsaliyeNo); break;
                    case ListOrderTypes.AdaGoreAzalan: queryable = queryable.OrderByDescending(x => x.IrsaliyeNo); break;

                }


                int itemCount = queryable.Count();
                var entityList = await queryable.Skip(pagedParam.PerPageItemCount * (pagedParam.CurrentPage - 1))
                                                .Take(pagedParam.PerPageItemCount).ToListAsync();


                var response = new PagedApiResponse<List<IrsaliyeListDto>>(true, mapper.Map<List<IrsaliyeListDto>>(entityList))
                {
                    CurrentPage = pagedParam.CurrentPage,
                    OrderType = pagedParam.OrderType,
                    PerPageItemCount = pagedParam.PerPageItemCount,
                    SearchText = pagedParam.SearchText,
                    PageCount = (int)Math.Ceiling(Convert.ToDecimal(itemCount) / Convert.ToDecimal(pagedParam.PerPageItemCount)),
                    Message = "irsaliyeler listelendi"
                };


                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<IrsaliyeListDto>> GetAllAsync()
        {
            try
            {
                var entity = await unit.IrsaliyeRepo.GetAllAsync().ToListAsync();
                return mapper.Map<List<IrsaliyeListDto>>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> ToplamIrsaliyeAdet()
        {
            try
            {
                var entity = await unit.IrsaliyeRepo.GetAllAsync().AsNoTracking().ToListAsync();
                return entity.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IrsaliyeListDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await unit.IrsaliyeRepo.GetByIdAsync(id);
                return mapper.Map<IrsaliyeListDto>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //WOLWOXTAN GELECEK TÜM İRSALİYELER İÇİN METOD İRSALİYENİN TÜRÜ VE PROJE KODU(OZEL KOD) ÖNEMLİ
        public async Task<List<EntegreResDto>> AddWolvoxAsync(List<IrsaliyeEntegreViewModelDto> dto)
        {
            List<EntegreResDto> irsaliyeler = new List<EntegreResDto>();
            try
            {
                foreach (var item in dto)
                {
                    var irsaliye = new Irsaliye();
                    var cari = await unit.CariRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.WolvoxBlKodu == item.Irsaliye.WolvoxCariBlKodu);
                    if(cari != null)
                    {
                        irsaliye.CariId = cari.ID;
                    }
                    irsaliye.WolvoxBlKodu = item.Irsaliye.WolvoxBlKodu;
                    irsaliye.ProjeNo = item.Irsaliye.OzelKod;
                    irsaliye.SevkAdres = item.Irsaliye.Adres;
                    if (item.Irsaliye.IrsaliyeTur == 2)
                    {
                        irsaliye.IrsaliyeTur = IrsaliyeTurEnum.Alis;
                    }
                    if (item.Irsaliye.IrsaliyeTur == 1)
                    {
                        irsaliye.IrsaliyeTur = IrsaliyeTurEnum.Satis;
                    }
                    irsaliye.IrsaliyeTarih = item.Irsaliye.Tarih;
                    irsaliye.SevkTarih = item.Irsaliye.SevkTarih;
                    irsaliye.IrsaliyeNo = item.Irsaliye.IrsaliyeNo;
                    irsaliye.WolvoxGonderimDurum = WolvoxDurumEnum.WolvoxaGonderildi;
                    var doviz = await unit.DovizRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x=>x.DovizBirim == item.Irsaliye.DovizBirim);
                    if(doviz != null)
                    {
                        irsaliye.DovizId = doviz.ID;
                    }
                    var returnedData = await unit.IrsaliyeRepo.AddIrsaliyeRawQuery(irsaliye);

                    foreach (var item2 in item.IrsaliyeDetay)
                    {
                        var irsaliyeDetay = new IrsaliyeDetay();
                        irsaliyeDetay.IrsaliyeId = returnedData.ID;
                        var stok = await unit.StokRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.WolvoxBlKodu == item2.WolvoxStokBlKodu);
                        if(stok != null)
                        {
                            irsaliyeDetay.StokId = stok.ID;
                        }
                        if(stok.Depo != null)
                        {
                            irsaliyeDetay.DepoId = stok.DepoId;
                        }
                        if (item2.Miktari != null)
                        {
                            irsaliyeDetay.Miktar = Convert.ToInt32(item2.Miktari);
                        }
                        await unit.IrsaliyeDetayRepo.AddIrsaliyeDetayRawQuery(irsaliyeDetay);
                    }
                    irsaliyeler.Add(new EntegreResDto { ID = returnedData.ID, WolvoxID = (long)returnedData.WolvoxBlKodu });

                    //ALINAN SATIŞ İRSALİYELERİ PROJE NO İLE SEVKİYATINA BAĞLANIR.
                    var sevkiyat = await unit.SevkiyatRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.ProjeNo == irsaliye.ProjeNo);
                    if (sevkiyat != null)
                    {
                        var sevkiyatDetay = new SevkiyatDetay();
                        sevkiyatDetay.SevkiyatId = sevkiyat.ID;
                        sevkiyatDetay.IrsaliyeId = returnedData.ID;
                        await unit.SevkiyatDetayRepo.AddAsync(sevkiyatDetay);
                        irsaliye.SevkDurum = IrsaliyeSevkDurumEnum.SevkiyataEklendi;
                        await unit.IrsaliyeRepo.AddIrsaliyeRawQuery(irsaliye);
                        await unit.SaveAsync();

                    }
                }

                return irsaliyeler;
            }
            catch (Exception ex)
            {
                throw new Exception("İrsaliye Eklerken Hata Oluştu." + "İrsaliye Eklerken Hata Oluştu.");
            }

        }
        public async Task<List<IrsaliyeEntegreViewModelDto>> SendWolvoxYeniIrsaliye()
        {
            try
            {
                var EntityList = await unit.IrsaliyeRepo.GetAllAsync().Include(x=>x.Cari)
                    .Include(x=>x.IrsaliyeDetay).ThenInclude(x=>x.Stok).Include(x=>x.IrsaliyeDetay).ThenInclude(x => x.Depo)
                    .Include(x => x.IrsaliyeDetay).ThenInclude(x => x.Hizmet).AsNoTracking().ToListAsync();

                List<IrsaliyeEntegreViewModelDto> yeniIrsaliyeListe = new List<IrsaliyeEntegreViewModelDto>();
                foreach (var item in EntityList)
                {
                    
                    if (item.WolvoxGonderimDurum == WolvoxDurumEnum.WolvoxaGonderilecek)
                    {
                        IrsaliyeEntegreViewModelDto yeniIrsaliye = new IrsaliyeEntegreViewModelDto();
                        yeniIrsaliye.Irsaliye.ID = item.ID;
                        yeniIrsaliye.Irsaliye.AdiSoyadi = item.Cari?.AdSoyad;
                        yeniIrsaliye.Irsaliye.WolvoxCariBlKodu = item.Cari?.WolvoxBlKodu;
                        yeniIrsaliye.Irsaliye.Adres = item.Cari?.Adres;
                        yeniIrsaliye.Irsaliye.TicariUnvan = item.Cari?.TicariUnvan;
                        yeniIrsaliye.Irsaliye.VergiAdresi = item.Cari?.VergiDairesi;
                        yeniIrsaliye.Irsaliye.VergiNo = item.Cari?.VergiNo;
                        yeniIrsaliye.Irsaliye.OzelKod = item.ProjeNo;
                        yeniIrsaliye.Irsaliye.IrsaliyeNo = item.IrsaliyeNo;
                        yeniIrsaliye.Irsaliye.SevkTarih = item.SevkTarih;
                       
                        foreach (var item2 in item.IrsaliyeDetay)
                        {
                            IrsaliyeDetayEntegreDto detay = new IrsaliyeDetayEntegreDto();
                            detay.ID = item2.ID;
                            if(item2.Stok != null)
                            {
                                detay.StokAdi = item2.Stok.StokAdi;
                                detay.WolvoxStokBlKodu = item2.Stok.WolvoxBlKodu;
                                detay.StokKodu = item2.Stok.StokKod;
                            }
                            if(item2.Hizmet != null)
                            {
                                detay.WolvoxHizmetBlKodu = item2.Hizmet.WolvoxBlKodu;
                            }
                            if(item2.Depo != null)
                            {
                                detay.DepoAdi = item2.Depo.DepoAd;
                            }
                            if(item2.Miktar != null)
                            {
                                detay.Miktari = Convert.ToString(item2.Miktar);
                            }
                            yeniIrsaliye.IrsaliyeDetay.Add(detay);
                            
                        }
                        yeniIrsaliyeListe.Add(yeniIrsaliye);
                    }
                }

                return yeniIrsaliyeListe;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task UpdateWolvoxBlKodu(List<BlKoduUpdateDto> dto)
        {
            var transaction = unit.BeginTransaction();
            try
            {
                foreach (var item in dto)
                {
                    var irsaliye = await unit.IrsaliyeRepo.GetByIdAsync((int)item.Id);
                    irsaliye.WolvoxBlKodu = item.WolvoxBlKodu;
                    irsaliye.WolvoxGonderimDurum = WolvoxDurumEnum.WolvoxaGonderildi;
                    irsaliye = await unit.IrsaliyeRepo.UpdateAsync(irsaliye);
                    await unit.SaveAsync();
                }
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
        }
        public async Task<List<EntegreResDto>> FaturalanmayanIrsaliyeListAsync()
        {
            var transaction = unit.BeginTransaction();
            try
            {
                var EntityList = await unit.IrsaliyeRepo.GetAllAsync().Include(x => x.Cari)
                    .Include(x => x.IrsaliyeDetay).ThenInclude(x => x.Stok)
                     .Include(x => x.IrsaliyeDetay).ThenInclude(x => x.Depo).AsNoTracking().ToListAsync();

                List<EntegreResDto> faturalanmamisIrsaliyeListe = new List<EntegreResDto>();
                foreach (var item in EntityList)
                {
                    if (item.FaturaDurum == IrsaliyeFaturaDurumEnum.FaturaBekliyor)
                    {
                        faturalanmamisIrsaliyeListe.Add(new EntegreResDto { WolvoxID = (long)item.WolvoxBlKodu});
                    }
                }
                await transaction.CommitAsync();
                return faturalanmamisIrsaliyeListe;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<IrsaliyeListDto> RemoveAsync(IrsaliyeInsertDto dto)
        {
            try
            {
                var Entity = mapper.Map<Irsaliye>(dto);
                Entity = await unit.IrsaliyeRepo.RemoveAsync(Entity);
                await unit.SaveAsync();
                return mapper.Map<IrsaliyeListDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IrsaliyeListDto> RemoveAsync(int id)
        {
            try
            {
                var Entity = await unit.IrsaliyeRepo.RemoveAsync(id);
                var irsaliyeDetay = await unit.IrsaliyeDetayRepo.GetAllAsync().AsNoTracking().Where(x => x.IrsaliyeId == Entity.ID).ToListAsync();
                var sevkiyatDetay = await unit.SevkiyatDetayRepo.GetAllAsync().AsNoTracking().Where(x => x.IrsaliyeId == id).ToListAsync();
                if(irsaliyeDetay != null)
                {
                    foreach (var item in irsaliyeDetay)
                    {
                        await unit.IrsaliyeDetayRepo.RemoveAsync(item.ID);
                    }
                }
                if(sevkiyatDetay != null)
                {
                    foreach (var item in sevkiyatDetay)
                    {
                        await unit.SevkiyatDetayRepo.RemoveAsync(item.ID);
                    }
                }
                await unit.SaveAsync();
                return mapper.Map<IrsaliyeListDto>(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IrsaliyeListViewModelDto> UpdateAsync(IrsaliyeInsertDto dto)// SEVKİYAT FİYAT GÜNCELLEMESİ OLABİLİR
        {
            try
            {
                var Entity = await unit.IrsaliyeRepo.GetByIdAsync(dto.ID);
                Entity.IrsaliyeNo = dto.IrsaliyeNo;
                Entity.ProjeNo = dto.ProjeNo;
                var sevkiyatDetay = await unit.SevkiyatDetayRepo.GetAllAsync().Include(x=>x.Sevkiyat).AsNoTracking().FirstOrDefaultAsync(x=>x.IrsaliyeId == Entity.ID);
                if(sevkiyatDetay != null && Entity.ProjeNo != sevkiyatDetay.Sevkiyat.ProjeNo)
                {
                    await unit.SevkiyatDetayRepo.RemoveAsync(sevkiyatDetay.ID);//sevkiyat varsa ve proje no aynı değilse proje no değişmiş demektir
                }
                //proje nolu irsaliyeyi bul ekle
                sevkiyatDetay = await unit.SevkiyatDetayRepo.GetAllAsync().Include(x => x.Sevkiyat).AsNoTracking().FirstOrDefaultAsync(x => x.Sevkiyat.ProjeNo == Entity.ProjeNo);
                if(sevkiyatDetay != null)
                {
                    var detay = new SevkiyatDetay();//yeni proje nolu sevkiyat varsa eskisinden sil buraya ekle 
                    detay.SevkiyatId = sevkiyatDetay.ID;
                    detay.IrsaliyeId = Entity.ID;
                    await unit.SevkiyatDetayRepo.AddAsync(detay);
                }
                Entity.IrsaliyeTarih = dto.IrsaliyeTarih;
                Entity.SevkTarih= dto.SevkTarih;
                Entity.SevkAdres = dto.SevkAdres;
                if(dto.CariId != null)
                {
                    Entity.CariId = dto.CariId;
                }
                Entity.WolvoxBlKodu=Entity.WolvoxBlKodu;
                if(Entity.DovizId != null)
                {
                    Entity.DovizId = Entity.DovizId;
                }
              
                Entity.SevkDurum = Entity.SevkDurum;
                Entity.IrsaliyeTur = Entity.IrsaliyeTur;
                Entity.FaturaDurum = Entity.FaturaDurum;
                Entity.WolvoxGonderimDurum = Entity.WolvoxGonderimDurum;
                Entity = await unit.IrsaliyeRepo.UpdateAsync(Entity);
                await unit.SaveAsync();
                var irsaliyeDetay = await unit.IrsaliyeDetayRepo.GetAllAsync().AsNoTracking().Where(x => x.IrsaliyeId == Entity.ID).ToListAsync();

                var IrsaliyeListViewModelDto = new IrsaliyeListViewModelDto();
                IrsaliyeListViewModelDto.Irsaliye = mapper.Map<IrsaliyeListDto>(Entity);
                IrsaliyeListViewModelDto.IrsaliyeDetay = mapper.Map<List<IrsaliyeDetayInsertDto>>(irsaliyeDetay);

                return IrsaliyeListViewModelDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
    }
}
