//Dùng để phân trang

namespace ClassManagement.Mvc.Models.Page
{
    public class PageViewModel : BasePageViewModel
    {
        public int TotalRecords { get; set; }
        public int PageCount
        {
            get
            {
                var pageCount = (double)TotalRecords / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }
    }
}
