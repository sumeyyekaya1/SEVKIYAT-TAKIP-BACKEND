using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IRepositories
{
    public interface IHizmetRepository:IRepository<Hizmet>
    {
        Task<Hizmet> AddHizmetRawQuery(Hizmet entity);
    }
}
