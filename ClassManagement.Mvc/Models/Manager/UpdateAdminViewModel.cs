using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClassManagement.Mvc.Models.Manager
{
    public class UpdateAdminViewModel
    {
        [BindNever]
        public string Email { get; set; }

        [BindNever]
        [Display(Name = "Tên người dùng")]
        public string UserName { get; set; }

        [Display(Name = "Tên đầy đủ")]
        [Required(ErrorMessage = "Vui lòng nhập tên đầy đủ")]
        public string Fullname { get; set; }
    }
}
