using ClassManagement.Mvc.Models.Page;
using ClassManagement.Mvc.Models.Role;
using ClassManagement.Mvc.Utilities;

namespace ClassManagement.Mvc.Integrations.Role
{
    class RoleHttpClientService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)

                : BaseHttpClientService(configuration, httpClientFactory, httpContextAccessor), IRoleHttpClientService
    {
        public async Task<PageResultViewModel<RoleViewModel>> GetAsync(CommonPageViewModel model)
        {
            var getRoleUrl = string.Format(ClassManagementMvcDef.GetRoles, ClassManagementMvcDef.RoleApi, model.Keyword, model.PageIndex, model.PageSize, model.SortOrder);

            var entities = await GetAsync<PageResultViewModel<RoleViewModel>>(getRoleUrl);

            return entities;
        }
    }
}
