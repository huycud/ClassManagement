using ClassManagement.Api.DTO.Common;

namespace ClassManagement.Api.DTO.Class
{
    public class ClassResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int ClassSize { get; set; }
        public TeacherItem TeacherItem { get; set; }
        public List<int>? HomeworksId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Type { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public int Amount { get; set; }
        public string Semester { get; set; }
        public string Subject { get; set; }
        public int Credit { get; set; }
        public string DayOfWeek { get; set; }
        public string ClassPeriods { get; set; }
    }
}
