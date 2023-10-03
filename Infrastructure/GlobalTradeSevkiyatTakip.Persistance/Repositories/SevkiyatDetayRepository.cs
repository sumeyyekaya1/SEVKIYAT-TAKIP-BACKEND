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
    public class SevkiyatDetayRepository : Repository<SevkiyatDetay>, ISevkiyatDetayRepository
    {
        public SevkiyatDetayRepository(EFDatabaseContext context) : base(context)
        {
        }
     
        public override Task<SevkiyatDetay> GetByIdAsync(int id)
        {
            return base.GetAllAsync().Include(x=>x.Irsaliye).ThenInclude(x=>x.Cari).AsNoTracking().FirstOrDefaultAsync(x=>x.ID == id);
        }
    }
}
