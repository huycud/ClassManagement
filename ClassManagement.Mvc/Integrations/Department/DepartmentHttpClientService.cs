using ClassManagement.Mvc.Models.Department;
using ClassManagement.Mvc.Models.Page;
using ClassManagement.Mvc.Utilities;

namespace ClassManagement.Mvc.Integrations.Department
{
    class DepartmentHttpClientService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)

        : BaseHttpClientService(configuration, httpClientFactory, httpContextAccessor), IDepartmentHttpClientService
    {
        public async Task<PageResultViewModel<DepartmentViewModel>> GetDepartmentAsync(CommonPageViewModel model)
        {
            var getDepartmentUrl = string.Format(ClassManagementMvcDef.GetDepartments, ClassManagementMvcDef.DepartmentApi, model.Keyword, model.PageIndex, model.PageSize, model.SortOrder);

            var entities = await GetAsync<PageResultViewModel<DepartmentViewModel>>(getDepartmentUrl);

            if (entities is null || entities.TotalRecords == 0) return new PageResultViewModel<DepartmentViewModel> { };

            return entities;
        }

        public async Task<DepartmentViewModel> GetByIdAsync(string id)
        {
            var getDepartmentIdUrl = string.Format(ClassManagementMvcDef.GetDepartmentId, ClassManagementMvcDef.DepartmentApi, id);

            var entity = await GetAsync<DepartmentViewModel>(getDepartmentIdUrl);

            return entity;
        }
    }
}
