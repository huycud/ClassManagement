using System.ComponentModel.DataAnnotations;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Mvc.Models.Subject
{
    public class CreateSubjectViewModel
    {
        [Display(Name = "Mã môn học")]
        [Required(ErrorMessage = "Vui lòng nhập mã môn học")]
        [StringLength(20, ErrorMessage = "Mã môn học phải ít hơn 20 kí tự.")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Mã môn học không được chứa kí tự đặc biệt")]
        public string Id { get; set; }

        [Display(Name = "Tên môn học")]
        [Required(ErrorMessage = "Vui lòng nhập tên môn học")]
        [StringLength(255, ErrorMessage = "Tên môn học phải ít hơn 255 kí tự.")]
        public string Name { get; set; }

        [Display(Name = "Khoa")]
        [Required(ErrorMessage = "Vui lòng chọn khoa")]
        public string DepartmentId { get; set; }

        [Display(Name = "Số tín chỉ")]
        [Required(ErrorMessage = "Vui lòng nhập số tín chỉ")]
        [Range(1, 10, ErrorMessage = "Vui lòng nhập giá trị trong khoảng {0} và {1}")]
        public int? Credit { get; set; }

        [Display(Name = "Trạng thái")]
        [Required(ErrorMessage = "Vui lòng chọn trạng thái")]
        [EnumDataType(typeof(Status), ErrorMessage = "Trạng thái không hợp lệ")]
        public Status? Status { get; set; }

        [Display(Name = "Hình thức")]
        [Required(ErrorMessage = "Vui lòng chọn có thực hành hay không?")]
        public bool? IsPracticed { get; set; }
    }
}
