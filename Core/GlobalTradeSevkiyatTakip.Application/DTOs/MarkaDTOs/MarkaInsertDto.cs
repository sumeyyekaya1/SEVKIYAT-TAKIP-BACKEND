﻿using GlobalTradeSevkiyatTakip.Application.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.MarkaDTOs
{
    public class MarkaInsertDto : BaseModelDto
    {
        public string? RenkAdi { get; set; }
    }
}
