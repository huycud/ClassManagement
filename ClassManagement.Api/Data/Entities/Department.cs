namespace ClassManagement.Api.Data.Entities
{
    public class Department : BaseIdTypeStringEntity
    {
        public virtual List<Subject> Subjects { get; set; }
        public virtual List<Client> Clients { get; set; }
    }
}
