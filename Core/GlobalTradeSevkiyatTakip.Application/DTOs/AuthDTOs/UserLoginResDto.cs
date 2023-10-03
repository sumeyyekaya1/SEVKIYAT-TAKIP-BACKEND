using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.DTOs.AuthDTOs
{
    public class UserLoginResDto<T>
    {
        public T User { get; set; }
        public string Token { get; set; }
    }
}
