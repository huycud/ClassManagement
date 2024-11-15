namespace ClassManagement.Api.DTO.Users
{
    public class BaseUserRequest
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
