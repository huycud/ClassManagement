using System.ComponentModel.DataAnnotations;

namespace ClassManagement.Mvc.Models.Clients
{
    public class BaseViewModel
    {
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [StringLength(255, ErrorMessage = "Tên phải nhiều hơn 2 và ít hơn 255 kí tự.", MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z0-9\\s]+$", ErrorMessage = "Tên không được chứa ki tự đặc biệt.")]
        public string Firstname { get; set; }

        [Display(Name = "Họ")]
        [Required(ErrorMessage = "Vui lòng nhập họ")]
        [StringLength(255, ErrorMessage = "Họ phải nhiều hơn 2 và ít hơn 255 kí tự.", MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z0-9\\s]+$", ErrorMessage = "Họ không được chứa ki tự đặc biệt.")]
        public string Lastname { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Vui lòng nhập đỉa chỉ")]
        [StringLength(255, ErrorMessage = "Địa chỉ phải ít hơn 255 kí tự.")]
        public string Address { get; set; }
    }
}
