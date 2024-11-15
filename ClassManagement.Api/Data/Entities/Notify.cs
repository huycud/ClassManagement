namespace ClassManagement.Api.Data.Entities
{
    public class Notify
    {
        public string Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
