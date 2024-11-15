namespace ClassManagement.Api.Data.Entities
{
    public class Image : BaseIdTypeIntEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public string Caption { get; set; }
        public long FileSize { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
