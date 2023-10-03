using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.RequestParameters
{
    public class PagedRequestParam
    {
        public int CurrentPage { get; set; } = 1;
        public int PerPageItemCount { get; set; } = 10;
        public int OrderType { get; set; } = 0;
        public int TotalItemCount { get; set; } = 0;
        public string SearchText { get; set; } = "";

        public PagedRequestParam() : this(1, 10, 0, 0, "") { }
        public PagedRequestParam(int currentPage) : this(currentPage, 10, 0, 0, "") { }
        public PagedRequestParam(int currentPage, int perPageItemCount) : this(currentPage, perPageItemCount, 0, 0, "") { }
        public PagedRequestParam(int currentPage, int perPageItemCount, int orderType) : this(currentPage, perPageItemCount, orderType, 0, "") { }
        public PagedRequestParam(int currentPage, int perPageItemCount, int orderType, int totalItemCount) : this(currentPage, perPageItemCount, orderType, totalItemCount, "") { }
        public PagedRequestParam(int currentPage, int perPageItemCount, int orderType, int totalItemCount, string searchText)
        {
            CurrentPage = currentPage;
            PerPageItemCount = perPageItemCount;
            OrderType = orderType;
            SearchText = searchText;
            TotalItemCount = totalItemCount;
        }
    }
}
