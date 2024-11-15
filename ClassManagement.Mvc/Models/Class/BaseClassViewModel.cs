using System.ComponentModel.DataAnnotations;

namespace ClassManagement.Mvc.Models.Class
{
    public class BaseClassViewModel
    {
        [Display(Name = "Tên lớp")]
        [Required(ErrorMessage = "Vui lòng nhập tên lớp")]
        [StringLength(255, ErrorMessage = "Tên lớp phải ít hơn 255 kí tự.")]
        public string Name { get; set; }

        [Display(Name = "Sĩ số")]
        [Required(ErrorMessage = "Vui lòng nhập sĩ số")]
        [Range(30, 150, ErrorMessage = "Vui lòng nhập giá trị trong khoảng {1} và {2}")]
        public int? ClassSize { get; set; }
    }
}
