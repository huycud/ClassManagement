namespace ClassManagement.Api.DTO.Users
{
    public class UserRolesResponse
    {
        public int Id { get; set; }
        public IList<string> Roles { get; set; }
    }
}
