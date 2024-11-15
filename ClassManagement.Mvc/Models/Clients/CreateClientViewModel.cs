using System.ComponentModel.DataAnnotations;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Mvc.Models.Clients
{
    public class CreateClientViewModel : BaseViewModel
    {
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Vui lòng nhập ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [StringLength(20, ErrorMessage = "Tên đăng nhập phải nhiều hơn 5 và ít hơn 20 kí tự.", MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Tên đăng nhập không được chứa kí tự đặc biệt.")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Required(ErrorMessage = "Vui lòng xác nhận lại mật khẩu")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        [EnumDataType(typeof(Gender), ErrorMessage = "Giới tính không hợp lệ.")]
        public Gender? Gender { get; set; }

        [Display(Name = "Khoa")]
        [Required(ErrorMessage = "Vui lòng chọn khoa")]
        public string DepartmentId { get; set; }

        [Display(Name = "Quyền")]
        [Required(ErrorMessage = "Vui lòng chọn quyền")]
        public string RoleName { get; set; }
    }
}
