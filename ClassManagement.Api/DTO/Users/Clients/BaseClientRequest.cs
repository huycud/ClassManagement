namespace ClassManagement.Api.DTO.Users.Clients
{
    public class BaseClientRequest : BaseUserRequest
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
