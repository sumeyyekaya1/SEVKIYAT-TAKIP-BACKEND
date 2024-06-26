﻿using GlobalTradeSevkiyatTakip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IRepositories
{
    public interface ICariRepository :IRepository<Cari>
    {
        Task<Cari> AddCariRawQuery(Cari entity);
    }
}
