using ClassManagement.Api.DTO.Department;
using ClassManagement.Api.DTO.Page;

namespace ClassManagement.Api.Services.Departments
{
    public interface IDepartmentService
    {
        Task<PageResult<DepartmentResponse>> GetAsync(CommonPageRequest request);
        Task<List<DepartmentResponse>> GetAsync();
        Task<DepartmentResponse> GetByIdAsync(string id);
        Task<string> CreateAsync(CreateDepartmentRequest request);
        Task<bool> UpdateAsync(string id, UpdateDepartmentRequest request);
    }
}
