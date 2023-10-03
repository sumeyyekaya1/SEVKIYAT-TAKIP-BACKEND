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
    public class IrsaliyeDetayRepository : Repository<IrsaliyeDetay>, IIrsaliyeDetayRepository
    {
        public IrsaliyeDetayRepository(EFDatabaseContext context) : base(context)
        {
        }
        public override Task<IrsaliyeDetay> GetByIdAsync(int id)
        {
            return base.GetAllAsync().Include(x=>x.Irsaliye).Include(x=>x.Hizmet).Include(x=>x.Stok).AsNoTracking().FirstOrDefaultAsync(x=>x.ID == id);
        }
        public async Task<IrsaliyeDetay> AddIrsaliyeDetayRawQuery(IrsaliyeDetay entity)
        {


            return await RawQuery($"IF(SELECT COUNT(*) FROM IrsaliyeDetay WHERE WolvoxBlKodu = @p0) = 0  " +
                   $"BEGIN " +
                   $"INSERT INTO [IrsaliyeDetay] ([WolvoxBlKodu], [IrsaliyeId], [StokId], [HizmetId], [DepoId], " +
                   $"[Miktar],[TakimDurum],[Icindekiler], [Olculer],[KapNo] ,[PaketSekil], [PaketIciAdet], [ToplamPaketIciAdet], " +
                   $"[KapAdet],[ToplamKapAdet],[TedarikFirma], [UrunIcerik], [UrunBurutAgirlik], [UrunNetAgirlik], " +
                   $"[OlusturanKullanici], [GuncelleyenKullanici], [GuncellemeTarih], [OlusturmaTarih], [IsDeleted]) " +
                   $"VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23); " +
                   $"SELECT * FROM IrsaliyeDetay where Id = scope_identity(); " +
                   $"END " +
                   $"ELSE " +
                   $"BEGIN " +
                   $"UPDATE IrsaliyeDetay SET " +
                   $"[WolvoxBlKodu]=@p0, [IrsaliyeId]=@p1, [StokId]=@p2, [HizmetId]=@p3, [DepoId]=@p4, " +
                   $"[Miktar]=@p5, [TakimDurum]=@p6, [Icindekiler]=@p7,[Olculer]=@p8,[KapNo]=@p9, [PaketSekil]=@p10, [PaketIciAdet]=@p11, [ToplamPaketIciAdet]=@p12, [KapAdet]=@p13, " +
                   $"[ToplamKapAdet]=@p14, [TedarikFirma]=@p15, [UrunIcerik]=@p16,[UrunBurutAgirlik]=@p17, [UrunNetAgirlik]=@p18,[OlusturanKullanici]=@p19, " +
                   $"[GuncelleyenKullanici]=@p20, [GuncellemeTarih]=@p21, [OlusturmaTarih]=@p22, [IsDeleted]=@p23 WHERE [WolvoxBlKodu]=@p0;" +
                   $"SELECT * FROM IrsaliyeDetay where [WolvoxBlKodu]=@p0; " +
                   $"END",
                   entity.WolvoxBlKodu, entity.IrsaliyeId, entity.StokId, entity.HizmetId, entity.DepoId, entity.Miktar, entity.TakimDurum,
                    entity.Icindekiler, entity.Olculer, entity.KapNo, entity.PaketSekil, entity.PaketIciAdet, entity.ToplamPaketIciAdet, entity.KapAdet, entity.ToplamKapAdet,
                   entity.TedarikFirma, entity.UrunIcerik, entity.UrunBurutAgirlik, entity.UrunNetAgirlik, entity.OlusturanKullanici,
                   entity.GuncelleyenKullanici, entity.GuncellemeTarih, entity.OlusturmaTarih, entity.IsDeleted);

        }
    }
}
