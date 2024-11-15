using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.DTO.Page
{
    public class CommonPageRequest : BasePageRequest
    {
        public SortOrder SortOrder { get; set; }
        public string? Keyword { get; set; }
    }
}
