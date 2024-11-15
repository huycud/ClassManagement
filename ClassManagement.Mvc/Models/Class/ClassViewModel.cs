using System.ComponentModel.DataAnnotations;
using ClassManagement.Api.DTO.Common;

namespace ClassManagement.Mvc.Models.Class
{
    public class ClassViewModel : BaseClassViewModel
    {
        [Display(Name = "Mã lớp")]
        public string Id { get; set; }

        [Display(Name = "Giáo viên")]
        public TeacherItem TeacherItem { get; set; }

        [Display(Name = "Danh sách bài tập")]
        public List<int>? HomeworksId { get; set; }

        [Display(Name = "Cập nhật")]
        public DateTime UpdatedAt { get; set; }

        [Display(Name = "Số lượng")]
        public int Amount { get; set; }

        [Display(Name = "Ngày bắt đầu")]
        public DateTime StartedAt { get; set; }

        [Display(Name = "Ngày kết thúc")]
        public DateTime EndedAt { get; set; }

        [Display(Name = "Học kỳ")]
        public string Scholastic { get; set; }

        [Display(Name = "Môn")]
        public string Subject { get; set; }

        [Display(Name = "Hình thức")]
        public string Type { get; set; }

        [Display(Name = "Ngày học")]
        public string DayOfWeek { get; set; }

        [Display(Name = "Tiết")]
        public string ClassPeriods { get; set; }
    }
}
