namespace ClassManagement.Api.DTO.Homeworks
{
    public class BaseHomeworkRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
    }
}
