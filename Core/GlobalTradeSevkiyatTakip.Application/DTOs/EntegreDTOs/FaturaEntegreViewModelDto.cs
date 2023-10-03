using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs
{
    public class FaturaEntegreViewModelDto
    {
        public FaturaEntegreDto? Fatura { get; set; }
        public List<FaturaDetayEntegreDto>? FaturaDetay { get; set; }
        public FaturaEntegreViewModelDto()
        {
            Fatura = new FaturaEntegreDto();
            FaturaDetay =new List<FaturaDetayEntegreDto>();
        }
    }
}
