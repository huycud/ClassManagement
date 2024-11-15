using System.ComponentModel.DataAnnotations;

namespace ClassManagement.Mvc.Models.Semester
{
    public class SemesterViewModel
    {
        [Display(Name = "Mã học kỳ")]
        public string Id { get; set; }

        [Display(Name = "Tên học kỳ")]
        public string Name { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime UpdatedAt { get; set; }
    }
}
