using GlobalTradeSevkiyatTakip.Application.Interfaces.IRepositories;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using GlobalTradeSevkiyatTakip.Persistance.EFContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Persistance.Repositories
{
    public class FaturaRepository : Repository<Fatura>, IFaturaRepository
    {
        public FaturaRepository(EFDatabaseContext context) : base(context)
        {
        }
        public override Task<Fatura> GetByIdAsync(int id)
        {
            return base.GetAllAsync().AsNoTracking().Include(x=>x.Cari).FirstOrDefaultAsync(x=>x.ID == id);
        }
        public async Task<Fatura> AddFaturaRawQuery(Fatura entity)
        {

            return await RawQuery($"IF(SELECT COUNT(*) FROM Fatura WHERE WolvoxBlKodu = @p0) = 0  " +
                   $"BEGIN " +
                   $"INSERT INTO [Fatura] ([WolvoxBlKodu],[CariId], [Tarih], [FaturaNo], [DovizId], [ProjeNo], " +
                   $"[WolvoxFaturaTutar], [WolvoxDolarTutar], [WolvoxDovizBirimTutar],[FaturaTur],[EvrakTur]," +
                   $" [OlusturanKullanici], [GuncelleyenKullanici], " +
                   $"[GuncellemeTarih], [OlusturmaTarih], [IsDeleted]) " +
                   $"VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15); " +
                   $"SELECT * FROM Fatura where Id = scope_identity(); " +
                   $"END " +
                   $"ELSE " +
                   $"BEGIN " +
                   $"UPDATE Fatura SET " +
                   $"[WolvoxBlKodu]=@p0, [CariId]=@p1, [Tarih]=@p2, [FaturaNo]=@p3, [DovizId]=@p4, " +
                   $"[ProjeNo]=@p5, [WolvoxFaturaTutar]=@p6, [WolvoxDolarTutar]=@p7,[WolvoxDovizBirimTutar]=@p8,[FaturaTur]=@p9," +
                   $"[EvrakTur]=@p10, [OlusturanKullanici]=@p11, " +
                   $"[GuncelleyenKullanici]=@p12, [GuncellemeTarih]=@p13, [OlusturmaTarih]=@p14, [IsDeleted]=@p15  WHERE [WolvoxBlKodu]=@p0;" +
                   $"SELECT * FROM Fatura where [WolvoxBlKodu]=@p0; " +
                   $"END",
                   entity.WolvoxBlKodu, entity.CariId, entity.Tarih, entity.FaturaNo, entity.DovizId, entity.ProjeNo, entity.WolvoxFaturaTutar,
                   entity.WolvoxDolarTutar, entity.WolvoxDovizBirimTutar, entity.FaturaTur, entity.EvrakTur,
                   entity.OlusturanKullanici, entity.GuncelleyenKullanici, entity.GuncellemeTarih, entity.OlusturmaTarih, entity.IsDeleted);
        }
    }
}
