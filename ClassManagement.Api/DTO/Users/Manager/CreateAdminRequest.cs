namespace ClassManagement.Api.DTO.Users.Manager
{
    public class CreateAdminRequest : BaseUserRequest
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Fullname { get; set; }
    }
}
