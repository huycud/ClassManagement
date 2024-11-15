using System.ComponentModel.DataAnnotations;

namespace ClassManagement.Mvc.Models.Notify
{
    public class BaseNotifyViewModel
    {
        [Display(Name = "Tiêu đề")]
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [StringLength(255, ErrorMessage = "Tiêu đề phải nhỏ hơn 255 ký tự")]
        public string Title { get; set; }
    }
}
