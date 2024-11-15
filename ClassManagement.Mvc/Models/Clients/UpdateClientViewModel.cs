using System.ComponentModel.DataAnnotations;

namespace ClassManagement.Mvc.Models.Clients
{
    public class UpdateClientViewModel
    {
        [Display(Name = "Mã")]
        public int Id { get; set; }

        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [StringLength(255, ErrorMessage = "Tên phải nhiều hơn 5 và ít hơn 20 kí tự.", MinimumLength = 2)]
        public string Firstname { get; set; }

        [Display(Name = "Họ")]
        [Required(ErrorMessage = "Vui lòng nhập họ")]
        [StringLength(255, ErrorMessage = "Họ phải nhiều hơn 2 và ít hơn 255 kí tự.", MinimumLength = 2)]
        public string Lastname { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Vui lòng nhập ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Khoa")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [StringLength(255, ErrorMessage = "Địa chỉ phải ít hơn 255 kí tự.")]
        public string Address { get; set; }
    }
}
