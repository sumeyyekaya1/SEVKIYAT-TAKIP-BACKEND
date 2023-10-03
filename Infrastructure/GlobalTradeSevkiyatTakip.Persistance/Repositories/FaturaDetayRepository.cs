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
    public class FaturaDetayRepository : Repository<FaturaDetay>, IFaturaDetayRepository
    {
        public FaturaDetayRepository(EFDatabaseContext context) : base(context)
        {
        }
        public async Task<FaturaDetay> AddFaturaDetayRawQuery(FaturaDetay entity)
        {

            return await RawQuery($"IF(SELECT COUNT(*) FROM FaturaDetay WHERE WolvoxBlKodu = @p0) = 0  " +
                   $"BEGIN " +
                   $"INSERT INTO [FaturaDetay] ([WolvoxBlKodu],[FaturaId], [StokId], [HizmetId], [Miktar], [ToplamTutar], " +
                   $"[OlusturanKullanici], [GuncelleyenKullanici], " +
                   $"[GuncellemeTarih], [OlusturmaTarih], [IsDeleted]) " +
                   $"VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10); " +
                   $"SELECT * FROM FaturaDetay where Id = scope_identity(); " +
                   $"END " +
                   $"ELSE " +
                   $"BEGIN " +
                   $"UPDATE FaturaDetay SET " +
                   $"[WolvoxBlKodu]=@p0, [FaturaId]=@p1, [StokId]=@p2, [HizmetId]=@p3, [Miktar]=@p4, " +
                   $"[ToplamTutar]=@p5, [OlusturanKullanici]=@p6, " +
                   $"[GuncelleyenKullanici]=@p7, [GuncellemeTarih]=@p8, [OlusturmaTarih]=@p9, [IsDeleted]=@p10  WHERE [WolvoxBlKodu]=@p0;" +
                   $"SELECT * FROM FaturaDetay where [WolvoxBlKodu]=@p0; " +
                   $"END",
                   entity.WolvoxBlKodu, entity.FaturaId, entity.StokId, entity.HizmetId, entity.Miktar, entity.ToplamTutar,
                   entity.OlusturanKullanici, entity.GuncelleyenKullanici, entity.GuncellemeTarih, entity.OlusturmaTarih, entity.IsDeleted);
        }
    }
}
