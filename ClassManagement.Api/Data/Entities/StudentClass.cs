namespace ClassManagement.Api.Data.Entities
{
    public class StudentClass
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string ClassId { get; set; }
        public virtual Class Class { get; set; }
    }
}
