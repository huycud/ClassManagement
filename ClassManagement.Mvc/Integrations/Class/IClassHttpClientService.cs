using ClassManagement.Mvc.Integrations.Common;
using ClassManagement.Mvc.Models.Class;
using ClassManagement.Mvc.Models.Clients;
using ClassManagement.Mvc.Models.Page;

namespace ClassManagement.Mvc.Integrations.Class
{
    public interface IClassHttpClientService : ICommonHttpClientService<ClassViewModel, string>
    {
        Task<PageResultViewModel<ClassViewModel>> GetClassesAsync(ClassPageViewModel model);
        Task<PageResultViewModel<ClientViewModel>> GetClientsByClassIdAsync(ClientsClassPageViewModel model);
        Task<PageResultViewModel<ClientViewModel>> GetStudentsNotExistInClassAsync(string id, ClientRolePageViewModel model);
        Task<object> CreateClassAsync(CreateClassViewModel model);
        Task<object> UpdateClassAsync(string id, UpdateClassViewModel model);
        Task<object> AddStudentsToClassAsync(string id, List<string> model);
    }
}
