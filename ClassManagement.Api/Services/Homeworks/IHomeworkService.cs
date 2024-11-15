using ClassManagement.Api.DTO.Homeworks;
using ClassManagement.Api.DTO.Page;

namespace ClassManagement.Api.Services.Homeworks
{
    public interface IHomeworkService
    {
        Task<HomeworkResponse> GetByIdAsync(int id);
        Task<List<HomeworkResponse>> GetHomeworksByClassId(string classId);
        Task<PageResult<HomeworkResponse>> GetAsync(UserPageRequest request);
        Task<bool> UpdateAsync(int id, UpdateHomeworkRequest request);
        Task<bool> DeleteAsync(int id);
        Task<int> AddHomeworkToClassAsync(int id, CreateHomeworkRequest request, CancellationToken cancellationToken);
    }
}
