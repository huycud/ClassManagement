namespace ClassManagement.Mvc.Models.Common.Errors
{
    public class ClassErrorViewModel : BaseErrorViewModel
    {
        public string? Name { get; set; }
        public string? ClassSize { get; set; }
        public string? StartedAt { get; set; }
        public string? EndedAt { get; set; }
        public string? TeacherId { get; set; }
        public string? ClassType { get; set; }
        public string? ScholasticId { get; set; }
        public string? SubjectId { get; set; }
        public string? Amount { get; set; }
        public string? TheoryClass { get; set; }
        public string? Exist { get; set; }
        public string? StudentsId { get; set; }
        public string? Related { get; set; }
        public string? DayOfWeek { get; set; }
        public string? ClassPeriods { get; set; }
    }
}
