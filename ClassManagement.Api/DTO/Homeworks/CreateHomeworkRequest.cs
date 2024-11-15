namespace ClassManagement.Api.DTO.Homeworks
{
    public class CreateHomeworkRequest : BaseHomeworkRequest
    {
        public string ClassId { get; set; }
        public IFormFile? File { get; set; }
    }
}
