﻿using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IRepositories
{
    public interface IDovizRepository : IRepository<Doviz>
    {
        Task<Doviz> AddDovizRawQuery(Doviz entity);
    }
}
