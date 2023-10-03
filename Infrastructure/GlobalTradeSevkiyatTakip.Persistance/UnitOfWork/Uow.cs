using GlobalTradeSevkiyatTakip.Application.Interfaces.IRepositories;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Persistance.EFContext;
using GlobalTradeSevkiyatTakip.Persistance.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Persistance.UnitOfWork
{
    public class Uow : IUOW
    {
        private EFDatabaseContext _context;
        private ICariRepository _cariRepo;
        private IDepoDetayRepository _depoDetayRepo;
        private IDepoRepository _depoRepo;
        private IIrsaliyeDetayRepository _irsaliyeDetayRepo;
        private IIrsaliyeRepository _irsaliyeRepo;
        private ISevkiyatDetayRepository _sevkiyatDetayRepo;
        private ISevkiyatRepository _sevkiyatRepo;
        private IStokRepository _stokRepo;
        private IHizmetRepository _hizmetRepo;
        private IStokDetayRepository _stokDetayRepo;
        private IRenkRepository _renkRepo;
        private IMarkaRepository _markaRepo;
        private IKullaniciRepository _kullaniciRepo;
        private IEntegreRepository _entegreKullaniciRepo;
        private ISevkiyatNotRepository _sevkiyatNotRepo;
        private IDovizRepository _dovizRepo;
        private IParaBirimRepository _paraBirimRepo;
        private IFaturaRepository _faturaRepo;
        private IFaturaDetayRepository _faturaDetayRepo;

        public Uow(EFDatabaseContext context)
        {
            this._context = context;
        }

        public ICariRepository CariRepo
        {
            get
            {
                if (_cariRepo == null)
                    _cariRepo = new CariRepository(_context);
                return _cariRepo;
            }
        }

        public IDepoDetayRepository DepoDetayRepo
        {
            get
            {
                if (_depoDetayRepo == null)
                    _depoDetayRepo = new DepoDetayRepository(_context);
                return _depoDetayRepo;
            }
        }

        public IDepoRepository DepoRepo
        {
            get
            {
                if (_depoRepo == null)
                    _depoRepo = new DepoRepository(_context);
                return _depoRepo;
            }
        }

        public IIrsaliyeDetayRepository IrsaliyeDetayRepo
        {
            get
            {
                if (_irsaliyeDetayRepo == null)
                    _irsaliyeDetayRepo = new IrsaliyeDetayRepository(_context);
                return _irsaliyeDetayRepo;
            }
        }

        public IIrsaliyeRepository IrsaliyeRepo
        {
            get
            {
                if (_irsaliyeRepo == null)
                    _irsaliyeRepo = new IrsaliyeRepository(_context);
                return _irsaliyeRepo;
            }
        }

        public ISevkiyatDetayRepository SevkiyatDetayRepo
        {
            get
            {
                if (_sevkiyatDetayRepo == null)
                    _sevkiyatDetayRepo = new SevkiyatDetayRepository(_context);
                return _sevkiyatDetayRepo;
            }
        }

        public ISevkiyatRepository SevkiyatRepo
        {
            get
            {
                if (_sevkiyatRepo == null)
                    _sevkiyatRepo = new SevkiyatRepository(_context);
                return _sevkiyatRepo;
            }
        }

        public IStokRepository StokRepo
        {
            get
            {
                if (_stokRepo == null)
                    _stokRepo = new StokRepository(_context);
                return _stokRepo;
            }
        }

        public IHizmetRepository HizmetRepo
        {
            get
            {
                if (_hizmetRepo == null)
                    _hizmetRepo = new HizmetRepository(_context);
                return _hizmetRepo;
            }
        }

        public IStokDetayRepository StokDetayRepo
        {
            get
            {
                if (_stokDetayRepo == null)
                    _stokDetayRepo = new StokDetayRepository(_context);
                return _stokDetayRepo;
            }
        }

        public IMarkaRepository MarkaRepo
        {
            get
            {
                if (_markaRepo == null)
                    _markaRepo = new MarkaRepository(_context);
                return _markaRepo;
            }
        }

        public IRenkRepository RenkRepo
        {
            get
            {
                if (_renkRepo == null)
                    _renkRepo = new RenkRepository(_context);
                return _renkRepo;
            }
        }

        public IKullaniciRepository KullaniciRepo
        {
            get
            {
                if (_kullaniciRepo == null)
                    _kullaniciRepo = new KullaniciRepository(_context);
                return _kullaniciRepo;
            }
        }

        public IEntegreRepository EntegreKullaniciRepo
        {
            get
            {
                if (_entegreKullaniciRepo == null)
                    _entegreKullaniciRepo = new EntegreRepository(_context);
                return _entegreKullaniciRepo;
            }
        }

        public ISevkiyatNotRepository SevkiyatNotRepo
        {
            get
            {
                if (_sevkiyatNotRepo == null)
                    _sevkiyatNotRepo = new SevkiyatNotRepository(_context);
                return _sevkiyatNotRepo;
            }
        }

        public IDovizRepository DovizRepo
        {
            get
            {
                if (_dovizRepo == null)
                    _dovizRepo = new DovizRepository(_context);
                return _dovizRepo;
            }
        }

        public IParaBirimRepository ParaBirimRepo
        {
            get
            {
                if (_paraBirimRepo == null)
                    _paraBirimRepo = new ParaBirimRepository(_context);
                return _paraBirimRepo;
            }
        }

        public IFaturaRepository FaturaRepo
        {
            get
            {
                if (_faturaRepo == null)
                    _faturaRepo = new FaturaRepository(_context);
                return _faturaRepo;
            }
        }

        public IFaturaDetayRepository FaturaDetayRepo
        {
            get
            {
                if (_faturaDetayRepo == null)
                    _faturaDetayRepo = new FaturaDetayRepository(_context);
                return _faturaDetayRepo;
            }
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
