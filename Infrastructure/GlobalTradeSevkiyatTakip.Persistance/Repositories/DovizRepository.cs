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
    public class DovizRepository : Repository<Doviz>, IDovizRepository
    {
        public DovizRepository(EFDatabaseContext context) : base(context)
        {
        }
        public async Task<Doviz> AddDovizRawQuery(Doviz entity)
        {

            return await RawQuery($"IF(SELECT COUNT(*) FROM Doviz WHERE WolvoxBlKodu = @p0) = 0  " +
                   $"BEGIN " +
                   $"INSERT INTO [Doviz] ([WolvoxBlKodu],[DovizBirim], [AlisFiyat], [SatisFiyat], [Tarih], [OzelKod], " +
                   $"[OlusturanKullanici], [GuncelleyenKullanici], " +
                   $"[GuncellemeTarih], [OlusturmaTarih], [IsDeleted]) " +
                   $"VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10); " +
                   $"SELECT * FROM Doviz where Id = scope_identity(); " +
                   $"END " +
                   $"ELSE " +
                   $"BEGIN " +
                   $"UPDATE Doviz SET " +
                   $"[WolvoxBlKodu]=@p0, [DovizBirim]=@p1, [AlisFiyat]=@p2, [SatisFiyat]=@p3, [Tarih]=@p4, " +
                   $"[OzelKod]=@p5, [OlusturanKullanici]=@p6, " +
                   $"[GuncelleyenKullanici]=@p7, [GuncellemeTarih]=@p8, [OlusturmaTarih]=@p9, [IsDeleted]=@p10  WHERE [WolvoxBlKodu]=@p0;" +
                   $"SELECT * FROM Doviz where [WolvoxBlKodu]=@p0; " +
                   $"END",
                   entity.WolvoxBlKodu, entity.DovizBirim, entity.AlisFiyat, entity.SatisFiyat, entity.Tarih, entity.OzelKod, 
                   entity.OlusturanKullanici, entity.GuncelleyenKullanici, entity.GuncellemeTarih, entity.OlusturmaTarih, entity.IsDeleted);
        }
    }
}
