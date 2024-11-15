using ClassManagement.Api.DTO.AppRole;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Users;

namespace ClassManagement.Api.Services.AppRole
{
    public interface IRoleService
    {
        Task<PageResult<RoleResponse>> GetAsync(CommonPageRequest request);
        Task<List<RoleResponse>> GetAsync();
        Task<RoleResponse> GetByIdAsync(int id);
        Task<UserRolesResponse> GetRolesByUserIdAsync(int id);
        Task<int> CreateAsync(CreateRoleRequest request);
        Task<bool> UpdateAsync(int id, UpdateRoleRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
