namespace ClassManagement.Mvc.Models.Common.Errors
{
    public class SubjectErrorViewModel : BaseErrorViewModel
    {
        public string? Name { get; set; }
        public string? DepartmentId { get; set; }
        public string? Credit { get; set; }
        public string? Status { get; set; }
        public string? IsPracticed { get; set; }
    }
}
