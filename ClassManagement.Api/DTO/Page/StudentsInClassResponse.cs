namespace ClassManagement.Api.DTO.Page
{
    public class StudentsInClassResponse
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string DepartmentName { get; set; }
        public string? ImagePath { get; set; }
    }
}
