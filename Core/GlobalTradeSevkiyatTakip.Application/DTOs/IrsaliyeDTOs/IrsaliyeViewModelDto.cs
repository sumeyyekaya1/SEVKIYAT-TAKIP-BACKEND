using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.IrsaliyeDTOs
{
    public class IrsaliyeViewModelDto
    {
        public IrsaliyeInsertDto? Irsaliye { get; set; }
        public List<IrsaliyeDetayInsertDto>? IrsaliyeDetay { get; set; }
        public IrsaliyeViewModelDto()
        {
            Irsaliye = new IrsaliyeInsertDto();
            IrsaliyeDetay = new List<IrsaliyeDetayInsertDto>();
        }
    }
}
