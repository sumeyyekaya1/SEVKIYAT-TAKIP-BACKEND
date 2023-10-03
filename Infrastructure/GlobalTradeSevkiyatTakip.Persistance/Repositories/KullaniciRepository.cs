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
    public class KullaniciRepository : Repository<Kullanici>, IKullaniciRepository
    {
        public KullaniciRepository(EFDatabaseContext context) : base(context)
        {
        }
        public async Task<Kullanici> GetByEPostaAsync(string eposta)
        {
            var personel = await base.GetAllAsync().AsNoTracking().Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Email.Equals(eposta));
            return personel;
        }
    }
}
