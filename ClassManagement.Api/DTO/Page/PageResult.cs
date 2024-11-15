namespace ClassManagement.Api.DTO.Page
{
    public class PageResult<T> : PageRequest
    {
        public List<T> Items { set; get; }
    }
}
