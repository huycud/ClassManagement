//Dùng để lấy danh sách notify bằng userId

using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Mvc.Models.Page
{
    public class NotifyPageViewModel : CommonPageViewModel
    {
        public int? UserId { get; set; }

        public NotifyType? Type { get; set; }

        public bool IsDeleted { get; set; }
    }
}
