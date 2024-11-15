using System.ComponentModel.DataAnnotations;

namespace ClassManagement.Mvc.Models.User
{
    public class UpdateImageViewModel
    {
        [Display(Name = "Select image")]
        public IFormFile Image { get; set; }
    }
}
