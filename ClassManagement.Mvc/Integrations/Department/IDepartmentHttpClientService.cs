using ClassManagement.Mvc.Integrations.Common;
using ClassManagement.Mvc.Models.Department;
using ClassManagement.Mvc.Models.Page;

namespace ClassManagement.Mvc.Integrations.Department
{
    public interface IDepartmentHttpClientService : ICommonHttpClientService<DepartmentViewModel, string>
    {
        Task<PageResultViewModel<DepartmentViewModel>> GetDepartmentAsync(CommonPageViewModel model);
    }
}
