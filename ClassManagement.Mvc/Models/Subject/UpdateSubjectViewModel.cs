using System.ComponentModel.DataAnnotations;

namespace ClassManagement.Mvc.Models.Subject
{
    public class UpdateSubjectViewModel
    {
        [Display(Name = "Mã môn học")]
        public string Id { get; set; }

        [Display(Name = "Tên môn học")]
        [Required(ErrorMessage = "Vui lòng nhập tên môn học")]
        [StringLength(255, ErrorMessage = "Tên môn học phải ít hơn 255 kí tự.")]
        public string Name { get; set; }

        [Display(Name = "Số tín chỉ")]
        [Required(ErrorMessage = "Vui lòng nhập số tín chỉ")]
        [Range(0, int.MaxValue, ErrorMessage = "Vui lòng nhập giá trị lớn hơn {1}.")]
        public int? Credit { get; set; }

        [Display(Name = "Trạng thái")]
        [Required(ErrorMessage = "Vui lòng chọn trạng thái")]
        public string Status { get; set; }

        [Display(Name = "Danh sách lớp")]
        public List<string>? ClassesId { get; set; }

        [Display(Name = "Khoa")]
        public string DepartmentId { get; set; }

        [Display(Name = "Hình thức")]
        [Required(ErrorMessage = "Vui lòng chọn có thực hành hay không?")]
        public string IsPracticed { get; set; }
    }
}
