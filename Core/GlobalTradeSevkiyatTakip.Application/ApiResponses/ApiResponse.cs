using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.ApiResponses
{
    public class ApiResponse<T> : BaseApiResponse
    {
        public T Value { get; set; }

        public ApiResponse(bool isSuccess, T value) : base(isSuccess, "")
        {
            this.Value = value;
        }

        public ApiResponse(bool isSuccess, T value, string message) : base(isSuccess, message)
        {
            this.Value = value;
        }
    }
}
