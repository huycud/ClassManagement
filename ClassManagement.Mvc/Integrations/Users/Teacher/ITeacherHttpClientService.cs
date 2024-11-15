using ClassManagement.Mvc.Integrations.Common;
using ClassManagement.Mvc.Models.Clients;

namespace ClassManagement.Mvc.Integrations.Users.Teacher
{
    public interface ITeacherHttpClientService : IProfileHttpClientService
    {
        Task<ClientViewModel> GetUserByUsernameAsync(string username);
    }
}
