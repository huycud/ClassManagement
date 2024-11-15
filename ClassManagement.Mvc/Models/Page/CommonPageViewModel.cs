//Dùng cho các model khác có phân trang kế thừa

using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Mvc.Models.Page
{
    public class CommonPageViewModel : BasePageViewModel
    {
        public SortOrder SortOrder { get; set; }
        public string? Keyword { get; set; }
    }
}
