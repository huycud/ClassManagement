namespace ClassManagement.Api.DTO.Class
{
    public class BaseClassRequest
    {
        public string Name { get; set; }
        public int ClassSize { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
    }
}
