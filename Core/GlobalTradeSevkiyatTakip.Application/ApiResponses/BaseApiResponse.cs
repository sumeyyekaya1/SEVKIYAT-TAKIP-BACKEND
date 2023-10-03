using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.ApiResponses
{
    public class BaseApiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Exception Error { get; set; }
        public int StatusCode { get; set; }

        public BaseApiResponse() : this(false) { }
        public BaseApiResponse(bool isSucces) : this(isSucces, "") { }

        public BaseApiResponse(bool isSuccess, string message)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
        }
    }
}
