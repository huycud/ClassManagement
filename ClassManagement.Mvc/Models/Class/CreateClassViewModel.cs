using System.ComponentModel.DataAnnotations;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Mvc.Models.Class
{
    public class CreateClassViewModel : BaseClassViewModel
    {
        [Display(Name = "Mã lớp")]
        [Required(ErrorMessage = "Vui lòng nhập mã lớp")]
        [StringLength(20, ErrorMessage = "Mã lớp phải ít hơn 20 kí tự.")]
        [RegularExpression("^[^!@#$%^&*()_+=\\[\\]{};':\"\\\\|,<>\\/?`~]*$", ErrorMessage = "Mã lớp không được chứa kí tự đặc biệt.")]
        public string Id { get; set; }

        [Display(Name = "Giáo viên")]
        [Required(ErrorMessage = "Vui lòng chọn giáo viên")]
        public int? TeacherId { get; set; }

        [Display(Name = "Môn học")]
        [Required(ErrorMessage = "Vui lòng chọn môn học")]
        public string SubjectId { get; set; }

        [Display(Name = "Hình thức")]
        [Required(ErrorMessage = "Vui lòng chọn hình thức")]
        [EnumDataType(typeof(ClassType), ErrorMessage = "Hình thức không hợp lệ.")]
        public ClassType? ClassType { get; set; }

        [Display(Name = "Học kỳ")]
        [Required(ErrorMessage = "Vui lòng chọn học kỳ")]
        public string SemesterId { get; set; }

        [Display(Name = "Ngày bắt đầu")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Vui lòng chọn ngày bắt đầu")]
        public DateTime? StartedAt { get; set; }

        [Display(Name = "Ngày kết thúc")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Vui lòng chọn ngày kết thúc")]
        [CheckIsValidEndedAt]
        public DateTime? EndedAt { get; set; }

        [Display(Name = "Ngày học")]
        [Required(ErrorMessage = "Vui lòng chọn ngày học")]
        //[EnumDataType(typeof(DayOfWeek), ErrorMessage = "Ngày học không hợp lệ.")]
        public string DayOfWeek { get; set; }

        [Display(Name = "Tiết học")]
        [Required(ErrorMessage = "Vui lòng chọn tiết học")]
        [EnumDataType(typeof(List<ClassPeriod>), ErrorMessage = "Tiết học không hợp lệ.")]
        public List<ClassPeriod> ClassPeriods { get; set; }
    }

    public class CheckIsValidEndedAtAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (CreateClassViewModel)validationContext.ObjectInstance;

            DateTime? startedAt = model.StartedAt;

            DateTime? endedAt = (DateTime?)value;

            if (endedAt.HasValue && startedAt.HasValue && endedAt < startedAt)

                return new ValidationResult("Ngày kết thúc phải lớn hơn ngày bắt đầu.");

            return ValidationResult.Success;
        }
    }
}
