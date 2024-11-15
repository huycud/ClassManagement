namespace ClassManagement.Api.Data.Entities
{
    public class PasswordResetToken : BaseIdTypeIntEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string ResetToken { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsUsed { get; set; }
    }
}
