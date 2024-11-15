//Dùng cho CommonPageViewModel và PageViewModel kế thừa

namespace ClassManagement.Mvc.Models.Page
{
    public class BasePageViewModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
