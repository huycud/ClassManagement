namespace ClassManagement.Api.DTO.Authentication
{
    public class RevokeRefreshTokenRequest
    {
        public int UserId { get; set; }
        public bool IsRevoked { get; set; }
    }
}
