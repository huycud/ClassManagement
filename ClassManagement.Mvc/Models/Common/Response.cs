namespace ClassManagement.Mvc.Models.Common
{
    public class Response
    {
        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }

        public int UserId { get; set; }

        public Response(string msg)
        {
            Message = msg;
        }
        public Response()
        {
            IsSuccess = true;
        }
    }
}
