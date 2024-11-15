using ClassManagement.Mvc.Integrations.Common;
using ClassManagement.Mvc.Models.Clients;
using ClassManagement.Mvc.Models.Manager;
using ClassManagement.Mvc.Models.Page;

namespace ClassManagement.Mvc.Integrations.Users.Manager
{
    public interface IAdminHttpClientService : IProfileHttpClientService, ICommonHttpClientService<AdminViewModel, int>
    {
        Task<ClientViewModel> GetClientByIdAsync(int id);
        Task<PageResultViewModel<ClientViewModel>> GetClientsByRoleNameAsync(ClientRolePageViewModel model);
        Task<object> CreateClientAsync(CreateClientViewModel model);
        Task<object> UpdateAsync(int id, UpdateAdminViewModel model);
        Task<object> UpdateClientAsync(int id, UpdateClientViewModel model);
    }
}
