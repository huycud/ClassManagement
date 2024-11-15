using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Semester;

namespace ClassManagement.Api.Services.Semesters
{
    public interface ISemesterService
    {
        Task<PageResult<SemesterResponse>> GetAsync(CommonPageRequest request);
        Task<List<SemesterResponse>> GetAsync();
        Task<SemesterResponse> GetByIdAsync(string id);
        Task<string> CreateAsync(CreateSemesterRequest request);
        Task<bool> UpdateAsync(string id, UpdateSemesterRequest request);
    }
}
