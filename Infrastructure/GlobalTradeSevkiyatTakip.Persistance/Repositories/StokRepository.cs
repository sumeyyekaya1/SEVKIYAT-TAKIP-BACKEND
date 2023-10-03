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
    public class StokRepository : Repository<Stok>, IStokRepository
    {
        public StokRepository(EFDatabaseContext context) : base(context)
        {
        }
        public async Task<Stok> AddStokRawQuery(Stok entity)
        {


            return await RawQuery($"IF(SELECT COUNT(*) FROM Stok WHERE WolvoxBlKodu = @p0) = 0  " +
                   $"BEGIN " +
                   $"INSERT INTO [Stok] ([WolvoxBlKodu], [StokKod], [Barkod], [StokAdi], [KdvOran], " +
                   $"[RenkId],[MarkaId],[DepoId], [IskontoOran], [Agirlik], [DesiMiktari], [DovizBirim], " +
                   $"[OlusturanKullanici], [GuncelleyenKullanici], [GuncellemeTarih], [OlusturmaTarih], [IsDeleted]) " +
                   $"VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15,@p16); " +
                   $"SELECT * FROM Stok where Id = scope_identity(); " +
                   $"END " +
                   $"ELSE " +
                   $"BEGIN " +
                   $"UPDATE Stok SET " +
                   $"[WolvoxBlKodu]=@p0, [StokKod]=@p1, [Barkod]=@p2, [StokAdi]=@p3, [KdvOran]=@p4, " +
                   $"[RenkId]=@p5, [MarkaId]=@p6, [DepoId]=@p7,[IskontoOran]=@p8, [Agirlik]=@p9, [DesiMiktari]=@p10, [DovizBirim]=@p11, [OlusturanKullanici]=@p12, " +
                   $"[GuncelleyenKullanici]=@p13, [GuncellemeTarih]=@p14, [OlusturmaTarih]=@p15, [IsDeleted]=@p16 WHERE [WolvoxBlKodu]=@p0;" +
                   $"SELECT * FROM Stok where [WolvoxBlKodu]=@p0; " +
                   $"END",
                   entity.WolvoxBlKodu, entity.StokKod, entity.Barkod, entity.StokAdi, entity.KdvOran, entity.RenkId,entity.MarkaId, entity.DepoId,
                   entity.IskontoOran, entity.Agirlik, entity.DesiMiktari, entity.DovizBirim,entity.OlusturanKullanici,
                   entity.GuncelleyenKullanici, entity.GuncellemeTarih, entity.OlusturmaTarih, entity.IsDeleted);

        }
    }
}
