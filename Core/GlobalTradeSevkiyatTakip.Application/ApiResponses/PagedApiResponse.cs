using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.ApiResponses
{
    public class PagedApiResponse<T> : ApiResponse<T>
    {
        public int CurrentPage { get; set; }
        public int PerPageItemCount { get; set; }
        public int TotalItemCount { get; set; }
        public int PageCount { get; set; }
        public int OrderType { get; set; } = 0;
        public string SearchText { get; set; }


        public PagedApiResponse(bool isSuccess, T value) : base(isSuccess, value)
        {
        }

        public PagedApiResponse(bool isSuccess, T value, int currentPage, int pageCount) : base(isSuccess, value)
        {
            CurrentPage = currentPage;
            PageCount = pageCount;
        }
    }
}
