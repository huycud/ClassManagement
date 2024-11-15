using ClassManagement.Mvc.Models.Page;
using ClassManagement.Mvc.Models.Role;

namespace ClassManagement.Mvc.Integrations.Role
{
    public interface IRoleHttpClientService
    {
        Task<PageResultViewModel<RoleViewModel>> GetAsync(CommonPageViewModel model);
    }
}
