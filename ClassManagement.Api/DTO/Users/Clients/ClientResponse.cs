namespace ClassManagement.Api.DTO.Users.Clients
{
    public class ClientResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string? ImagePath { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
