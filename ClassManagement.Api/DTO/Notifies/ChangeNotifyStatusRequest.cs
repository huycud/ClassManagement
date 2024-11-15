namespace ClassManagement.Api.DTO.Notifies
{
    public class ChangeNotifyStatusRequest
    {
        public int UserId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
