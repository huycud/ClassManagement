namespace ClassManagement.Mvc.Models.Manager
{
    public class AdminViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Fullname { get; set; }

        public string UserName { get; set; }

        public string? ImagePath { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
