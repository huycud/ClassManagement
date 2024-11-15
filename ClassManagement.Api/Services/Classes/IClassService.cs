using ClassManagement.Api.DTO.Class;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Users.Clients;

namespace ClassManagement.Api.Services.Classes
{
    public interface IClassService
    {
        Task<PageResult<ClassResponse>> GetAsync(ClassPageRequest request);
        Task<ClassResponse> GetByIdAsync(string id);
        Task<PageResult<ClassResponse>> GetByClientIdAsync(ClassesClientPageRequest request);
        Task<PageResult<ClientResponse>> GetStudentsNotExistInClassAsync(string id, UserPageRequest request);
        Task<string> CreateAsync(CreateClassRequest request);
        Task<bool> UpdateAsync(string id, UpdateClassRequest request);
        Task<bool> DeleteAsync(string id);
        Task<bool> AddStudentToClassAsync(string id, List<int> request);
    }
}
