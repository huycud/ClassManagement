using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.DTO.Page
{
    public class TeacherNotifyPageRequest : CommonPageRequest
    {
        public NotifyType? Type { get; set; }
    }
}
