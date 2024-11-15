using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.DTO.Class
{
    public class CreateClassRequest : BaseClassRequest
    {
        public string Id { get; set; }
        public int UserId { get; set; }
        public string SubjectId { get; set; }
        public ClassType Type { get; set; }
        public string SemesterId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public List<ClassPeriod> ClassPeriods { get; set; }
    }
}
