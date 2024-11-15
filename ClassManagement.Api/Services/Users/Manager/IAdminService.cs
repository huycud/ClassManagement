using ClassManagement.Api.DTO.AppRole;
using ClassManagement.Api.DTO.Common;
using ClassManagement.Api.DTO.Users.Manager;

namespace ClassManagement.Api.Services.Users.Manager
{
    public interface IAdminService : IProfileService
    {
        Task<AdminResponse> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateAdminRequest request);
        Task<bool> UpdateAsync(int id, UpdateAdminRequest request);
        Task<bool> AddRoleAsync(int id, RoleRequest request);
        Task<bool> RemoveRoleAsync(int id, RoleRequest request);
        Task<bool> DisableAccountAsync(int id, DisableAccountRequest request);
    }
}
