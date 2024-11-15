using ClassManagement.Mvc.Models.Clients;
using ClassManagement.Mvc.Models.Common;
using ClassManagement.Mvc.Models.Manager;
using ClassManagement.Mvc.Models.Page;
using ClassManagement.Mvc.Models.User;
using ClassManagement.Mvc.Utilities;

namespace ClassManagement.Mvc.Integrations.Users.Manager
{
    class AdminHttpClientService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)

        : UserHttpClientService(configuration, httpClientFactory, httpContextAccessor), IAdminHttpClientService
    {
        public async Task<object> CreateClientAsync(CreateClientViewModel model)
        {
            GetSession();

            var dataContent = model.GetRequestMultipartFormContent(string.Empty);

            var response = await _httpClient.PostAsync(ClassManagementMvcDef.ClientApi, dataContent);

            if (!response.IsSuccessStatusCode) return response;

            return new Response();
        }

        public async Task<AdminViewModel> GetByIdAsync(int id)
        {
            var getAdminIdUrl = string.Format(ClassManagementMvcDef.GetClientId, ClassManagementMvcDef.AdminApi, id);

            var entity = await GetAsync<AdminViewModel>(getAdminIdUrl);

            if (entity is null) return new AdminViewModel { };

            var newPath = $"{_configuration["Host:BaseApi"]}{entity.ImagePath}";

            entity.ImagePath = newPath;

            return entity;
        }

        public async Task<ClientViewModel> GetClientByIdAsync(int id)
        {
            var getClientIdUrl = string.Format(ClassManagementMvcDef.GetClientId, ClassManagementMvcDef.ClientApi, id);

            var entity = await GetAsync<ClientViewModel>(getClientIdUrl);

            if (entity is null) return new ClientViewModel { };

            if (!string.IsNullOrEmpty(entity.ImagePath)) entity.ImagePath = $"{_configuration["BaseApi"]}{entity.ImagePath}";

            return entity;
        }

        //Dùng để hiển thị danh sách clients với key = roleName
        public async Task<PageResultViewModel<ClientViewModel>> GetClientsByRoleNameAsync(ClientRolePageViewModel model)
        {
            var getClientsByRoleUrl = string.Format(ClassManagementMvcDef.GetClientsByRole, ClassManagementMvcDef.ClientApi, model.RoleName, model.Keyword,

                                    model.PageIndex, model.PageSize, model.SortOrder, model.IsDisabled);

            var entities = await GetAsync<PageResultViewModel<ClientViewModel>>(getClientsByRoleUrl);

            if (entities is null || entities.TotalRecords == 0) return new PageResultViewModel<ClientViewModel> { };

            foreach (var entity in entities.Items)
            {
                if (!string.IsNullOrEmpty(entity.ImagePath)) entity.ImagePath = $"{_configuration["BaseApi"]}{entity.ImagePath}";
            }

            return entities;
        }

        public async Task<object> UpdateAsync(int id, UpdateAdminViewModel model)
        {
            GetSession();

            var getAdminIdUrl = string.Format(ClassManagementMvcDef.GetClientId, ClassManagementMvcDef.AdminApi, id);

            var response = await _httpClient.PutAsync(getAdminIdUrl, HandleRequest.RequestHandler(model));

            if (!response.IsSuccessStatusCode) return response;

            return new Response { };
        }

        public async Task<object> UpdatePasswordAsync(int id, UpdatePasswordViewModel model)
        {
            var getUpdatePasswordUrl = string.Format(ClassManagementMvcDef.GetUpdatePassword, ClassManagementMvcDef.AdminApi, id);

            return await UpdatePasswordAsync(getUpdatePasswordUrl, model);
        }

        public async Task<object> UpdateClientAsync(int id, UpdateClientViewModel model)
        {
            GetSession();

            var dataContent = model.GetRequestMultipartFormContent(string.Empty);

            var getClientIdUrl = string.Format(ClassManagementMvcDef.GetClientId, ClassManagementMvcDef.ClientApi, id);

            var response = await _httpClient.PutAsync(getClientIdUrl, dataContent);

            if (!response.IsSuccessStatusCode) return response;

            return new Response { };
        }

        public async Task<object> UpdateAvatarAsync(int id, UpdateImageViewModel model)
        {
            var getUpdateAvatarUrl = string.Format(ClassManagementMvcDef.GetUpdateAvatar, ClassManagementMvcDef.AdminApi, id);

            return await UpdateImageAsync(getUpdateAvatarUrl, model);
        }
    }
}
