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
    public class IrsaliyeRepository : Repository<Irsaliye>, IIrsaliyeRepository
    {
        public IrsaliyeRepository(EFDatabaseContext context) : base(context)
        {
        }
        public override Task<Irsaliye> GetByIdAsync(int id)
        {
            return base.GetAllAsync().Include(x=>x.Cari).Include(x => x.Doviz).Include(x => x.IrsaliyeDetay).AsNoTracking().FirstOrDefaultAsync(x=>x.ID == id);
        }
        public async Task<Irsaliye> AddIrsaliyeRawQuery(Irsaliye entity)
        {


            return await RawQuery($"IF(SELECT COUNT(*) FROM Irsaliye WHERE WolvoxBlKodu = @p0) = 0 " +
                   $"BEGIN " +
                   $"INSERT INTO [Irsaliye] ([WolvoxBlKodu], [ProjeNo], [IrsaliyeNo], [IrsaliyeTarih], [SevkTarih], " +
                   $"[SevkAdres],[CariId],[DovizId],[SevkDurum], " +
                   $"[IrsaliyeTur], [FaturaDurum], [WolvoxGonderimDurum], " +
                   $"[OlusturanKullanici], [GuncelleyenKullanici], [GuncellemeTarih], [OlusturmaTarih], [IsDeleted]) " +
                   $"VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15,@p16); " +
                   $"SELECT * FROM Irsaliye where Id = scope_identity(); " +
                   $"END " +
                   $"ELSE " +
                   $"BEGIN " +
                   $"UPDATE Irsaliye SET " +
                   $"[WolvoxBlKodu]=@p0, [ProjeNo]=@p1, [IrsaliyeNo]=@p2, [IrsaliyeTarih]=@p3, [SevkTarih]=@p4, " +
                   $"[SevkAdres]=@p5, [CariId]=@p6,[DovizId]=@p7," +
                   $"[SevkDurum]=@p8, [IrsaliyeTur]=@p9, [FaturaDurum]=@p10, [WolvoxGonderimDurum]=@p11, [OlusturanKullanici]=@p12, " +
                   $"[GuncelleyenKullanici]=@p13, [GuncellemeTarih]=@p14, [OlusturmaTarih]=@p15, [IsDeleted]=@p16 WHERE [WolvoxBlKodu]=@p0;" +
                   $"SELECT * FROM Irsaliye where [WolvoxBlKodu]=@p0; " +
                   $"END",
                   entity.WolvoxBlKodu, entity.ProjeNo, entity.IrsaliyeNo, entity.IrsaliyeTarih, entity.SevkTarih, entity.SevkAdres, entity.CariId,
                   entity.DovizId,entity.SevkDurum, entity.IrsaliyeTur,
                   entity.FaturaDurum, entity.WolvoxGonderimDurum, entity.OlusturanKullanici,
                   entity.GuncelleyenKullanici, entity.GuncellemeTarih, entity.OlusturmaTarih, entity.IsDeleted);

        }
    }
}
