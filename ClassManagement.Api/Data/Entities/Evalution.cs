namespace ClassManagement.Api.Data.Entities
{
    public class Evalution : BaseIdTypeIntEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string AcademicAbility { get; set; }
        public string MoralTraining { get; set; }
    }
}
