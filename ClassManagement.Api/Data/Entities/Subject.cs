namespace ClassManagement.Api.Data.Entities
{
    public class Subject : BaseIdTypeStringEntity
    {
        public int Credit { get; set; }
        public string Status { get; set; }
        public string DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual List<Class>? Classes { get; set; }
        public virtual List<Score>? Scores { get; set; }
        public bool IsPracticed { get; set; }
    }
}
