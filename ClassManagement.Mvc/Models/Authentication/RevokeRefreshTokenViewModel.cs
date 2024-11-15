namespace ClassManagement.Mvc.Models.Authentication
{
    public class RevokeRefreshTokenViewModel
    {
        public int UserId { get; set; }
        public bool IsRevoked { get; set; }
    }
}
