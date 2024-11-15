using System.ComponentModel.DataAnnotations;

namespace ClassManagement.Mvc.Models.Class
{
    public class UpdateClassViewModel : BaseClassViewModel
    {
        [Display(Name = "Mã lớp")]
        public string Id { get; set; }

        [Display(Name = "Giáo viên")]
        [Required(ErrorMessage = "Vui lòng chọn giáo viên")]
        public int? TeacherId { get; set; }

        [Display(Name = "Môn học")]
        public string Subject { get; set; }

        [Display(Name = "Học kỳ")]
        public string Scholastic { get; set; }

        [Display(Name = "Ngày bắt đầu")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Vui lòng chọn ngày bắt đầu")]
        public DateTime? StartedAt { get; set; }

        [Display(Name = "Ngày kết thúc")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Vui lòng chọn ngày kết thúc")]
        public DateTime? EndedAt { get; set; }

        [Display(Name = "Hình thức")]
        public string Type { get; set; }
    }
}
