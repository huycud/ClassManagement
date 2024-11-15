namespace ClassManagement.Api.DTO.Subject
{
    public class SubjectResponse : BaseSubjectRequest
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Status { get; set; }
        public int Credit { get; set; }
        public List<string>? ClassesId { get; set; }
        public string DepartmentId { get; set; }
        public int ScoreId { get; set; }
        public string IsPracticed { get; set; }
    }
}
