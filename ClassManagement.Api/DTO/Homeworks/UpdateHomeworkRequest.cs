namespace ClassManagement.Api.DTO.Homeworks
{
    public class UpdateHomeworkRequest : BaseHomeworkRequest
    {
        public IFormFile? File { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
