using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ClassManagement.Mvc.Models.Semester
{
    public class CreateSemesterViewModel
    {
        [Display(Name = "Mã học kỳ")]
        [Required(ErrorMessage = "Vui lòng nhập mã học kỳ")]
        //[JsonProperty(PropertyName = "Id")]
        //public string SemesterId { get; set; }
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Mã học kỳ không được chứa kí tự đặc biệt.")]
        public string Id { get; set; }

        [Display(Name = "Tên học kỳ")]
        [Required(ErrorMessage = "Vui lòng nhập tên học kỳ")]
        [StringLength(255, ErrorMessage = "Tên học kỳ phải nhỏ hơn 255 kí tự.")]
        public string Name { get; set; }
    }
}
