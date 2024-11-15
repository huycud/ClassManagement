namespace ClassManagement.Api.Data.Entities
{
    public class Homework : BaseIdTypeIntEntity
    {
        public string ClassId { get; set; }
        public virtual Class Class { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? FilePath { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
    }
}
