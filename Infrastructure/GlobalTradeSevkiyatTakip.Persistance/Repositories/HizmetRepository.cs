using GlobalTradeSevkiyatTakip.Application.Interfaces.IRepositories;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using GlobalTradeSevkiyatTakip.Persistance.EFContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Persistance.Repositories
{
    public class HizmetRepository : Repository<Hizmet>, IHizmetRepository
    {
        public HizmetRepository(EFDatabaseContext context) : base(context)
        {
        }
        public async Task<Hizmet> AddHizmetRawQuery(Hizmet entity)
        {

            return await RawQuery($"IF(SELECT COUNT(*) FROM Hizmet WHERE WolvoxBlKodu = @p0) = 0  " +
                   $"BEGIN " +
                   $"INSERT INTO [Hizmet] ([WolvoxBlKodu],[HizmetAdi],[OlusturanKullanici],[GuncelleyenKullanici],[GuncellemeTarih],[OlusturmaTarih],[IsDeleted]) " +
                   $"VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6); " +
                   $"SELECT * FROM Hizmet where Id = scope_identity(); " +
                   $"END " +
                   $"ELSE " +
                   $"BEGIN " +
                   $"UPDATE Hizmet SET " +
                   $"[WolvoxBlKodu]=@p0,[HizmetAdi]=@p1, [OlusturanKullanici]=@p2,[GuncelleyenKullanici]=@p3,[GuncellemeTarih]=@p4, " +
                   $"[OlusturmaTarih]=@p5,[IsDeleted]=@p6 WHERE [WolvoxBlKodu]=@p0;" +
                   $"SELECT * FROM Hizmet where [WolvoxBlKodu]=@p0; " +
                   $"END",
                   entity.WolvoxBlKodu, entity.HizmetAdi,entity.OlusturanKullanici, entity.GuncelleyenKullanici, entity.GuncellemeTarih, entity.OlusturmaTarih, entity.IsDeleted);
        }
    }
}
