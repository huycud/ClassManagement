//Dùng để lấy danh sách subject bằng departmentId

namespace ClassManagement.Mvc.Models.Page
{
    public class SubjectPageViewModel : CommonPageViewModel
    {
        public string? DepartmentId { get; set; }
    }
}
