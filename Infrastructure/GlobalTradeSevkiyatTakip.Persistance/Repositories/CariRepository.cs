using GlobalTradeSevkiyatTakip.Application.Interfaces.IRepositories;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using GlobalTradeSevkiyatTakip.Persistance.EFContext;

namespace GlobalTradeSevkiyatTakip.Persistance.Repositories
{
    public class CariRepository : Repository<Cari>, ICariRepository
    {
        public CariRepository(EFDatabaseContext context) : base(context)
        {
        }
        public async Task<Cari> AddCariRawQuery(Cari entity)
        {

            return await RawQuery($"IF(SELECT COUNT(*) FROM Cari WHERE WolvoxBlKodu = @p0) = 0  " +
                   $"BEGIN " +
                   $"INSERT INTO [Cari] ([WolvoxBlKodu],[CariKodu], [Ad], [Soyad], [TicariUnvan], [Email], " +
                   $"[TcNo], [VergiDairesi], [VergiNo], [Adres], [Iletisim], [OlusturanKullanici], [GuncelleyenKullanici], " +
                   $"[GuncellemeTarih], [OlusturmaTarih], [IsDeleted]) " +
                   $"VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15); " +
                   $"SELECT * FROM Cari where Id = scope_identity(); " +
                   $"END " +
                   $"ELSE " +
                   $"BEGIN " +
                   $"UPDATE Cari SET " +
                   $"[WolvoxBlKodu]=@p0, [CariKodu]=@p1, [Ad]=@p2, [Soyad]=@p3, [TicariUnvan]=@p4, " +
                   $"[Email]=@p5, [TcNo]=@p6, [VergiDairesi]=@p7, [VergiNo]=@p8, [Adres]=@p9, [Iletisim]=@p10, [OlusturanKullanici]=@p11, " +
                   $"[GuncelleyenKullanici]=@p12, [GuncellemeTarih]=@p13, [OlusturmaTarih]=@p14, [IsDeleted]=@p15  WHERE [WolvoxBlKodu]=@p0;" +
                   $"SELECT * FROM Cari where [WolvoxBlKodu]=@p0; " +
                   $"END",
                   entity.WolvoxBlKodu,entity.CariKodu, entity.Ad, entity.Soyad, entity.TicariUnvan, entity.Email, entity.TcNo, entity.VergiDairesi, entity.VergiNo, entity.Adres, entity.Iletisim,
                   entity.OlusturanKullanici, entity.GuncelleyenKullanici, entity.GuncellemeTarih, entity.OlusturmaTarih,entity.IsDeleted);
        }
    }
}
