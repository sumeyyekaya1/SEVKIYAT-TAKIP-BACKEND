using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IRepositories
{
    public interface IIrsaliyeDetayRepository:IRepository<IrsaliyeDetay>
    {
        Task<IrsaliyeDetay> AddIrsaliyeDetayRawQuery(IrsaliyeDetay entity);
    }
}
