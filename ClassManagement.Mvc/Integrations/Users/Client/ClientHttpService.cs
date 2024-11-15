using ClassManagement.Mvc.Models.Class;
using ClassManagement.Mvc.Models.Clients;
using ClassManagement.Mvc.Models.Common;
using ClassManagement.Mvc.Models.Page;
using ClassManagement.Mvc.Models.User;
using ClassManagement.Mvc.Utilities;

///<summary>
/// Các phương thức mà Teacher và Student đều sử dụng
/// </summary>
namespace ClassManagement.Mvc.Integrations.Users.Client
{
    class ClientHttpService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)

        : UserHttpClientService(configuration, httpClientFactory, httpContextAccessor), IClientHttpService
    {
        public async Task<ClientViewModel> GetByIdAsync(int id)
        {
            var getClientUrl = string.Format(ClassManagementMvcDef.GetClientId, ClassManagementMvcDef.ClientApi, id);

            var entity = await GetAsync<ClientViewModel>(getClientUrl);

            if (entity is null) return new ClientViewModel();

            if (!string.IsNullOrEmpty(entity.ImagePath))
            {
                var newPath = $"{_configuration["Host:BaseApi"]}{entity.ImagePath}";

                entity.ImagePath = newPath;
            }

            return entity;
        }

        public async Task<PageResultViewModel<ClassViewModel>> GetClassesByClientIdAsync(ClassesClientPageViewModel model)
        {
            var getClassesByClientUrl = string.Format(ClassManagementMvcDef.GetClassesByClient, ClassManagementMvcDef.ClassApi, model.ClientId, model.Keyword,

                                        model.PageIndex, model.PageSize, model.SortOrder);

            var entities = await GetAsync<PageResultViewModel<ClassViewModel>>(getClassesByClientUrl);

            return entities;
        }

        public async Task<object> UpdateAsync(int id, UpdateClientViewModel model)
        {
            GetSession();

            var getClientUrl = string.Format(ClassManagementMvcDef.GetClientId, ClassManagementMvcDef.ClientApi, id);

            var response = await _httpClient.PutAsync(getClientUrl, HandleRequest.RequestHandler(model));

            if (!response.IsSuccessStatusCode) return response;

            return new Response();
        }

        public async Task<object> UpdateAvatarAsync(int id, UpdateImageViewModel model)
        {
            var getUpdateAvatarUrl = string.Format(ClassManagementMvcDef.GetUpdateAvatar, ClassManagementMvcDef.ClientApi, id);

            return await UpdateImageAsync(getUpdateAvatarUrl, model);
        }

        public async Task<object> UpdatePasswordAsync(int id, UpdatePasswordViewModel model)
        {
            var getUpdatePasswordUrl = string.Format(ClassManagementMvcDef.GetUpdatePassword, ClassManagementMvcDef.ClientApi, id);

            return await UpdatePasswordAsync(getUpdatePasswordUrl, model);
        }
    }
}
