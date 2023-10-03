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
    public class EntegreRepository : Repository<Entegre>, IEntegreRepository
    {
        public EntegreRepository(EFDatabaseContext context) : base(context)
        {
        }

        public async Task<Entegre> GetByEPostaAsync(string eposta)
        {
            var entegreKullanici = await base.GetAllAsync().AsNoTracking().FirstOrDefaultAsync(x => x.Email.Equals(eposta));
            return entegreKullanici;
        }
    }
}
