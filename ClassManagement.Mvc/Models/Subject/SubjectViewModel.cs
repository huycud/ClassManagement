using System.ComponentModel.DataAnnotations;

namespace ClassManagement.Mvc.Models.Subject
{
    public class SubjectViewModel
    {
        [Display(Name = "Mã môn học")]
        public string Id { get; set; }

        [Display(Name = "Tên môn học")]
        public string Name { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime UpdatedAt { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; }

        [Display(Name = "Số tín chỉ")]
        public int Credit { get; set; }

        [Display(Name = "Lớp liên quan")]
        public List<string> ClassesId { get; set; }

        [Display(Name = "Khoa")]
        public string DepartmentId { get; set; }

        [Display(Name = "Hình thức")]
        public string IsPracticed { get; set; }
    }
}
