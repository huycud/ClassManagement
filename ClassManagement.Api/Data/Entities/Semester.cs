namespace ClassManagement.Api.Data.Entities
{
    public class Semester : BaseIdTypeStringEntity
    {
        public virtual List<Class> Classes { get; set; }
    }
}
