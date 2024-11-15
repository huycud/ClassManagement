namespace ClassManagement.Api.Data.Entities
{
    public class Admin : BaseIdTypeIntEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Fullname { get; set; }
    }
}
