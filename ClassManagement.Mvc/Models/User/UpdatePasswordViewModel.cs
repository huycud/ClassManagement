using System.ComponentModel.DataAnnotations;

namespace ClassManagement.Mvc.Models.User
{
    public class UpdatePasswordViewModel
    {
        [Display(Name = "Current Password")]
        [Required(ErrorMessage = "Please enter current password")]
        public string CurrentPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "Please enter new password")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please enter confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
