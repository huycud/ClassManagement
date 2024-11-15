using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.DTO.Notifies
{
    public class UpdateNotifyRequest : BaseNotifyRequest
    {
        public string Content { get; set; }
        public NotifyType Type { get; set; }
    }
}
