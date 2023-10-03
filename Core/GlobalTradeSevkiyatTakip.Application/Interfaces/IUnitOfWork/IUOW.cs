using GlobalTradeSevkiyatTakip.Application.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork
{
    public interface IUOW
    {
       ICariRepository CariRepo { get; }
       IDepoDetayRepository DepoDetayRepo { get; }
       IDepoRepository DepoRepo { get; }
       IIrsaliyeDetayRepository IrsaliyeDetayRepo { get; }
       IIrsaliyeRepository IrsaliyeRepo { get; }
       ISevkiyatDetayRepository SevkiyatDetayRepo { get; }
       ISevkiyatRepository SevkiyatRepo { get; }
       IStokRepository StokRepo { get; }
       IHizmetRepository HizmetRepo { get; }
       IStokDetayRepository StokDetayRepo { get; }
       IMarkaRepository MarkaRepo { get; }
       IRenkRepository RenkRepo { get; }
       IKullaniciRepository KullaniciRepo { get; }
       IEntegreRepository EntegreKullaniciRepo { get; }
       ISevkiyatNotRepository SevkiyatNotRepo { get; }
       IDovizRepository DovizRepo { get; }
       IParaBirimRepository ParaBirimRepo { get; }
       IFaturaRepository FaturaRepo { get; }
       IFaturaDetayRepository FaturaDetayRepo { get; }

       IDbContextTransaction BeginTransaction();
       Task<int> SaveAsync();
     
    }
}
