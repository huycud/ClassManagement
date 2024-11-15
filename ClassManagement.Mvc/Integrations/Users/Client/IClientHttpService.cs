using ClassManagement.Mvc.Integrations.Common;
using ClassManagement.Mvc.Models.Class;
using ClassManagement.Mvc.Models.Clients;
using ClassManagement.Mvc.Models.Page;

namespace ClassManagement.Mvc.Integrations.Users.Client
{
    public interface IClientHttpService : IProfileHttpClientService, ICommonHttpClientService<ClientViewModel, int>
    {
        Task<PageResultViewModel<ClassViewModel>> GetClassesByClientIdAsync(ClassesClientPageViewModel model);
        Task<object> UpdateAsync(int id, UpdateClientViewModel model);
    }
}
