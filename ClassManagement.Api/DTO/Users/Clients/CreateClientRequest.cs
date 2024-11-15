using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.DTO.Users.Clients
{
    public class CreateClientRequest : BaseClientRequest
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public Gender Gender { get; set; }
        public string DepartmentId { get; set; }
        public string RoleName { get; set; }
    }
}
