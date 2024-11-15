namespace ClassManagement.Api.Data.Entities
{
    public class Client : BaseIdTypeIntEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
