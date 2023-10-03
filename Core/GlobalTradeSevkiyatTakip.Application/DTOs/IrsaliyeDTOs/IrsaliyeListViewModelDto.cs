using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.IrsaliyeDTOs
{
    public class IrsaliyeListViewModelDto
    {
        public IrsaliyeListDto? Irsaliye { get; set; }
        public List<IrsaliyeDetayInsertDto>? IrsaliyeDetay { get; set; }
        public IrsaliyeListViewModelDto()
        {
            Irsaliye = new IrsaliyeListDto();
            IrsaliyeDetay = new List<IrsaliyeDetayInsertDto>();
        }
    }
}
