using GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.ParaBirimDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface IParaBirimServis
    {
        Task<List<ParaBirimListDto>> GetAllAsync();
    }
}
