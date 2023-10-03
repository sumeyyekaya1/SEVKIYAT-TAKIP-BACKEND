using AutoMapper;
using GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.DovizDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Persistance.Services
{
    public class DovizServis : IDovizServis
    {
        private readonly IUOW unit;
        private readonly IMapper mapper;
        public DovizServis(IUOW unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        public async Task<DovizListDto> AddAsync(DovizInsertDto dto)
        {
            try
            {
                var entity = mapper.Map<Doviz>(dto);
                entity = await unit.DovizRepo.AddAsync(entity);
                await unit.SaveAsync();
                return mapper.Map<DovizListDto>(entity);
            }
            catch (Exception)
            { throw; }
        }
        public async Task<List<EntegreResDto>> AddWolvoxAsync(List<DovizEntegreDto> dto)
        {
            List<EntegreResDto> dovizler = new List<EntegreResDto>();
            var transaction = unit.BeginTransaction();
            try
            {
                foreach (var item in dto)
                {
                    Doviz doviz = new Doviz();
                    doviz.WolvoxBlKodu = item.WolvoxBlKodu;
                    doviz.DovizBirim = item.DovizBirim;
                    doviz.AlisFiyat = item.AlisFiyat;
                    doviz.SatisFiyat = item.SatisFiyat;
                    doviz.Tarih=item.Tarih;
                    doviz.OzelKod=item.OzelKod;
                    await AddParaBirimWolvoxAsync(item.DovizBirim);
                    var sendedDoviz = await unit.DovizRepo.AddDovizRawQuery(doviz);
                    var returnedValue = new EntegreResDto { ID = sendedDoviz.ID, WolvoxID = (long)sendedDoviz.WolvoxBlKodu };
                    dovizler.Add(returnedValue);
                }
                await transaction.CommitAsync();
                return dovizler;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Döviz Kur Eklerken Hata Oluştu." + "Döviz Kur Eklerken Hata Oluştu.");
            }
        }
        private async Task AddParaBirimWolvoxAsync(string? paraBirim)
        {
            try
            {
                if (!string.IsNullOrEmpty(paraBirim))
                {
                    var birim = await unit.ParaBirimRepo.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.BirimSimge == paraBirim);
                    if (birim == null)
                    {
                        birim = await unit.ParaBirimRepo.AddAsync(new ParaBirim { BirimSimge = paraBirim });
                        await unit.SaveAsync();
                    }
                  
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<DovizListDto>> GetAllAsync()
        {
            try
            {
                var entity = await unit.DovizRepo.GetAllAsync().GroupBy(x=>x.DovizBirim).ToListAsync();
                return mapper.Map<List<DovizListDto>>(entity);
            }
            catch (Exception)
            { throw; }
        }

        public async Task<DovizListDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await unit.DovizRepo.GetByIdAsync(id);
                return mapper.Map<DovizListDto>(entity);
            }
            catch (Exception)
            { throw; }
        }

        public async Task<DovizListDto> RemoveAsync(DovizInsertDto dto)
        {
            try
            {
                var Entity = mapper.Map<Doviz>(dto);
                Entity = await unit.DovizRepo.RemoveAsync(Entity);
                await unit.SaveAsync();
                return mapper.Map<DovizListDto>(Entity);
            }
            catch (Exception)
            { throw; }
        }

        public async Task<DovizListDto> RemoveAsync(int id)
        {
            try
            {
                var Entity = await unit.DovizRepo.RemoveAsync(id);
                await unit.SaveAsync();
                return mapper.Map<DovizListDto>(Entity);
            }
            catch (Exception)
            { throw; }
        }

        public async Task<DovizListDto> UpdateAsync(DovizInsertDto dto)
        {
            try
            {
                var Entity = mapper.Map<Doviz>(dto);
                Entity = await unit.DovizRepo.UpdateAsync(Entity);
                await unit.SaveAsync();
                return mapper.Map<DovizListDto>(Entity);
            }
            catch (Exception)
            { throw; }
        }
    }
}
