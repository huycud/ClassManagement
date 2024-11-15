using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.DTO.Page
{
    public class NotifyPageRequest : CommonPageRequest
    {
        public int? UserId { get; set; }
        public NotifyType? Type { get; set; }
        public bool IsDeleted { get; set; }
    }
}
