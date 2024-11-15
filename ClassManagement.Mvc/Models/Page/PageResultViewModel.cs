//Dùng để hiện thị danh sách của các model có phân trang

namespace ClassManagement.Mvc.Models.Page
{
    public class PageResultViewModel<T> : PageViewModel
    {
        public List<T>? Items { set; get; }
    }
}
