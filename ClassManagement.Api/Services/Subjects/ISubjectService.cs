using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Subject;

namespace ClassManagement.Api.Services.Subjects
{
    public interface ISubjectService
    {
        Task<PageResult<SubjectResponse>> GetAsync(SubjectDepartmentPageRequest request);
        Task<List<SubjectResponse>> GetAsync();
        Task<SubjectResponse> GetByIdAsync(string id);
        Task<string> CreateAsync(CreateSubjectRequest request);
        Task<bool> UpdateAsync(string id, UpdateSubjectRequest request);
    }
}
