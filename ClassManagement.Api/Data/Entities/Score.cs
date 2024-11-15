namespace ClassManagement.Api.Data.Entities
{
    public class Score : BaseIdTypeIntEntity
    {
        public int Process { get; set; }
        public int MidTerm { get; set; }
        public int FinalTest { get; set; }
        public float Average { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
