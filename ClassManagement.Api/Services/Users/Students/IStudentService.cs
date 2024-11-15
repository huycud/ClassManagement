using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Users.Clients;
using ClassManagement.Api.Services.Users.Clients;

namespace ClassManagement.Api.Services.Users.Students
{
    public interface IStudentService
    {
        Task<PageResult<StudentsInClassResponse>> GetStudentsByClassIdAsync(int id, StudentPageRequest request);
    }
}
