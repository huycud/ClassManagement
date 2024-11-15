namespace ClassManagement.Api.DTO.Sender
{
    public class EmailInfo
    {
        public string From { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Provider { get; set; }
    }
}
