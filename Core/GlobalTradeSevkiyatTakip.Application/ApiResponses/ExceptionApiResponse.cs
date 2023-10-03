using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.ApiResponses
{
    public class ExceptionApiResponse<T> : BaseApiResponse
    {
        public T? Value { get; set; }
        public string? InnerExceptionMessage { get; set; }
        public ExceptionApiResponse(string message, string innerExceptionMessage) : base(false, message)
        {
            this.InnerExceptionMessage = innerExceptionMessage;
        }
        public ExceptionApiResponse(string message, T value, string innerExceptionMessage) : base(false, message)
        {
            this.InnerExceptionMessage = innerExceptionMessage;
            this.Value = value;
        }
    }
}
