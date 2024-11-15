namespace ClassManagement.Api.DTO.Users.Clients
{
    public class UpdateClientRequest
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}
