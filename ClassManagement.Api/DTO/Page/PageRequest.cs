namespace ClassManagement.Api.DTO.Page
{
    public class PageRequest : BasePageRequest
    {
        public int TotalRecords { get; set; }
        public int PageCount
        {
            get
            {
                var pageCount = (double)TotalRecords / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }
    }
}
