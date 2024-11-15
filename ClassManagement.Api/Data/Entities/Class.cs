namespace ClassManagement.Api.Data.Entities
{
    public class Class : BaseIdTypeStringEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        public int ClassSize { get; set; }
        public int Amount { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public virtual List<StudentClass>? StudentClasses { get; set; }
        public virtual List<Homework>? Homeworks { get; set; }
        public string SemesterId { get; set; }
        public virtual Semester Semester { get; set; }
        public string Type { get; set; }
        public int Credit { get; set; }
        public string DayOfWeek { get; set; }
        public string ClassPeriods { get; set; }
    }
}
