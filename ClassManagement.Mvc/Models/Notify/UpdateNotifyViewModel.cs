using System.ComponentModel.DataAnnotations;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Mvc.Models.Notify
{
    public class UpdateNotifyViewModel : BaseNotifyViewModel
    {
        [Display(Name = "Mã thông báo")]
        public string Id { get; set; }

        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        public string Content { get; set; }

        [Display(Name = "Loại thông báo")]
        [Required(ErrorMessage = "Vui lòng chọn loại thông báo")]
        [EnumDataType(typeof(NotifyType), ErrorMessage = "Loại thông báo không hợp lệ")]
        public NotifyType? Type { get; set; }
    }
}
