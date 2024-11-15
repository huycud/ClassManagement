using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.DTO.Subject
{
    public class CreateSubjectRequest : BaseSubjectRequest
    {
        public string Id { get; set; }
        public string DepartmentId { get; set; }
        public int Credit { get; set; }
        public Status Status { get; set; }
        public bool IsPracticed { get; set; }
    }
}
