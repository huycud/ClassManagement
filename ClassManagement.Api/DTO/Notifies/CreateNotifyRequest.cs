using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.DTO.Notifies
{
    public class CreateNotifyRequest : BaseNotifyRequest
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public NotifyType Type { get; set; }
    }
}
