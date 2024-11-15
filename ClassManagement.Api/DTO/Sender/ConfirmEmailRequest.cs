namespace ClassManagement.Api.DTO.Sender
{
    public class ConfirmEmailRequest
    {
        public string Email { get; set; }
        public string ConfirmEmailToken { get; set; }
    }
}
