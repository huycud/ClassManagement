namespace ClassManagement.Mvc.Models.Homework
{
    public class HomeworkViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public string ClassName { get; set; }
        public string? FilePath { get; set; }
    }
}
