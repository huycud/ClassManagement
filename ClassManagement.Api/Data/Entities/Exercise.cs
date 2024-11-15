namespace ClassManagement.Api.Data.Entities
{
    public class Exercise : BaseIdTypeIntEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string? FilePath { get; set; }
    }
}
