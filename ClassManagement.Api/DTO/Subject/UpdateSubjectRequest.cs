using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.DTO.Subject
{
    public class UpdateSubjectRequest : BaseSubjectRequest
    {
        public int Credit { get; set; }
        public Status Status { get; set; }
        public bool IsPracticed { get; set; }
    }
}
