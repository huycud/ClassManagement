using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.DTO.Page
{
    public class UserPageRequest : BasePageRequest
    {
        public string? SortProperty { get; set; }
        public SortOrder SortOrder { get; set; }
        public string? Keyword { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
