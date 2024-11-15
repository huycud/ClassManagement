using ClassManagement.Mvc.Models.Clients;
using ClassManagement.Mvc.Models.Common;
using ClassManagement.Mvc.Models.User;
using ClassManagement.Mvc.Utilities;

namespace ClassManagement.Mvc.Integrations.Users.Teacher
{
    class TeacherHttpClientService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) 
        
        : UserHttpClientService(configuration, httpClientFactory, httpContextAccessor), ITeacherHttpClientService
    {
        public async Task<ClientViewModel> GetUserByUsernameAsync(string username)
        {

            var entity = await GetAsync<ClientViewModel>($"api/teachers/info/{username}");

            var newPath = $"{_configuration["Host:BaseApi"]}{entity.ImagePath}";

            entity.ImagePath = newPath;

            return entity;
        }

        public async Task<object> UpdatePasswordAsync(int id, UpdatePasswordViewModel model)
        {
            var getUpdatePasswordUrl = string.Format(ClassManagementMvcDef.GetUpdatePassword, ClassManagementMvcDef.TeacherApi, id);

            return await UpdatePasswordAsync(getUpdatePasswordUrl, model);
        }

        //public async Task<ClientViewModel> GetUserByIdAsync(int id)
        //{
        //    var entity = await GetAsync<ClientViewModel>($"api/teachers/{id}");

        //    if (!string.IsNullOrEmpty(entity.ImagePath))
        //    {
        //        var newPath = $"{_configuration["BaseAddress"]}{entity.ImagePath}";

        //        entity.ImagePath = newPath;
        //    }

        //    return entity;
        //}

        public async Task<object> UpdateAsync(int id, UpdateClientViewModel model)
        {
            GetSession();

            var getTeacherIdUrl = string.Format(ClassManagementMvcDef.GetClientId, ClassManagementMvcDef.TeacherApi, id);

            var response = await _httpClient.PutAsync(getTeacherIdUrl, HandleRequest.RequestHandler(model));

            if (!response.IsSuccessStatusCode) return response;

            return new Response();
        }

        public async Task<object> UpdateAvatarAsync(int id, UpdateImageViewModel model)
        {
            var getUpdateAvatarUrl = string.Format(ClassManagementMvcDef.GetUpdateAvatar, ClassManagementMvcDef.TeacherApi, id);

            return await UpdateImageAsync(getUpdateAvatarUrl, model);
        }
    }
}
