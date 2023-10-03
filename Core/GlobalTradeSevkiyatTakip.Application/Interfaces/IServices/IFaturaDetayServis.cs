using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.FaturaDTOs;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Interfaces.IServices
{
    public interface IFaturaDetayServis
    {
        Task<PagedApiResponse<List<FaturaDetayListDto>>> GetPagedListAsync(PagedRequestParam pagedParam, int faturaId);
    }
}
