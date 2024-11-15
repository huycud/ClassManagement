using System.ComponentModel.DataAnnotations;

namespace ClassManagement.Mvc.Models.Clients
{
    public class ClientViewModel : BaseViewModel
    {
        [Display(Name = "Mã")]
        public int Id { get; set; }

        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Giới tính")]
        public string Gender { get; set; }

        [Display(Name = "Hình ảnh")]
        public string? ImagePath { get; set; }

        [Display(Name = "Khoa")]
        public string DepartmentName { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime UpdatedAt { get; set; }
    }
}
