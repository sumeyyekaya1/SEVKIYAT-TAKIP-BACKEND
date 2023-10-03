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
    public class ParaBirimRepository : Repository<ParaBirim>, IParaBirimRepository
    {
        public ParaBirimRepository(EFDatabaseContext context) : base(context)
        {
        }

    }
}
