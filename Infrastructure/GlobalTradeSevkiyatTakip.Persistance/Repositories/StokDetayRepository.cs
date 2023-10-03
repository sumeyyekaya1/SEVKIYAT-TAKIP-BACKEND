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
    public class StokDetayRepository : Repository<StokDetay>, IStokDetayRepository
    {
        public StokDetayRepository(EFDatabaseContext context) : base(context)
        {
        }
        public async Task<StokDetay> AddStokDetayRawQuery(StokDetay entity)
        {


            return await RawQuery($"IF(SELECT COUNT(*) FROM StokDetay WHERE StokId = @p3 AND CariId =@p2 AND DepoId = @p1) = 0  " +
                   $"BEGIN " +
                   $"INSERT INTO [StokDetay] ([WolvoxBlKodu], [DepoId], [CariId], [StokId], [GirisMiktar],[CikisMiktar],[NetMiktar], " +
                   $"[OlusturanKullanici], [GuncelleyenKullanici], [GuncellemeTarih], [OlusturmaTarih], [IsDeleted]) " +
                   $"VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11); " +
                   $"SELECT * FROM StokDetay where Id = scope_identity(); " +
                   $"END " +
                   $"ELSE " +
                   $"BEGIN " +
                   $"UPDATE StokDetay SET " +
                   $"[WolvoxBlKodu]=@p0, [DepoId]=@p1, [CariId]=@p2, [StokId]=@p3, [GirisMiktar]=[GirisMiktar] + @p4, [CikisMiktar]=[CikisMiktar]+@p5, [NetMiktar]=([GirisMiktar] + @p4)-([CikisMiktar]+@p5), " +
                   $"[OlusturanKullanici]=@p7, [GuncelleyenKullanici]=@p8, [GuncellemeTarih]=@p9, [OlusturmaTarih]=@p10, [IsDeleted]=@p11 WHERE [StokId]=@p3 AND CariId =@p2 AND DepoId = @p1;" +
                   $"SELECT * FROM StokDetay where [StokId]=@p3 AND CariId =@p2 AND DepoId = @p1; " +
                   $"END",
                   entity.WolvoxBlKodu, entity.DepoId, entity.CariId, entity.StokId, entity.GirisMiktar, entity.CikisMiktar, 
                   entity.NetMiktar, entity.OlusturanKullanici, entity.GuncelleyenKullanici, entity.GuncellemeTarih,
                   entity.OlusturmaTarih, entity.IsDeleted);

        }
    }
}
