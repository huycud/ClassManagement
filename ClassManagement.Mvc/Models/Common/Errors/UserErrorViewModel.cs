namespace ClassManagement.Mvc.Models.Common.Errors
{
    public class UserErrorViewModel
    {
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Address { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Image { get; set; }
        public string? Fullname { get; set; }
        public string? DepartmentId { get; set; }
        public string? Gender { get; set; }
        public string? RoleName { get; set; }
    }
}
