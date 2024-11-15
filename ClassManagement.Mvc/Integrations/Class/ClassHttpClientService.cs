using ClassManagement.Mvc.Models.Class;
using ClassManagement.Mvc.Models.Clients;
using ClassManagement.Mvc.Models.Common;
using ClassManagement.Mvc.Models.Page;
using ClassManagement.Mvc.Utilities;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Mvc.Integrations.Class
{
    class ClassHttpClientService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)

                : BaseHttpClientService(configuration, httpClientFactory, httpContextAccessor), IClassHttpClientService
    {
        public async Task<object> AddStudentsToClassAsync(string id, List<string> model)
        {
            GetSession();

            List<int> dataIdArray = [];

            Mapper.Map(dataIdArray, model);

            var getAddStudentsToClassUrl = string.Format(ClassManagementMvcDef.GetAddStudentsToClass, ClassManagementMvcDef.ClassApi, id);

            var response = await _httpClient.PostAsync(getAddStudentsToClassUrl, HandleRequest.RequestHandler(dataIdArray));

            if (!response.IsSuccessStatusCode) return response;

            return new Response { };
        }

        public async Task<object> CreateClassAsync(CreateClassViewModel model)
        {
            GetSession();

            var dataContent = model.GetRequestMultipartFormContent(string.Empty);

            var response = await _httpClient.PostAsync(ClassManagementMvcDef.ClassApi, dataContent);

            if (!response.IsSuccessStatusCode) return response;

            return new Response { };
        }

        public async Task<ClassViewModel> GetByIdAsync(string id)
        {
            var getClassIdUrl = string.Format(ClassManagementMvcDef.GetClassId, ClassManagementMvcDef.ClassApi, id);

            var entity = await GetAsync<ClassViewModel>(getClassIdUrl);

            if (entity is null) return new ClassViewModel { };

            entity.Type = entity.Type.Equals(ClassType.Practice.ToString()) ? "Thực hành" : "Lý thuyết";

            return entity;
        }

        public async Task<PageResultViewModel<ClassViewModel>> GetClassesAsync(ClassPageViewModel model)
        {
            var getClassesBySubjectUrl = string.Format(ClassManagementMvcDef.GetClassesBySubject, ClassManagementMvcDef.ClassApi, model.SubjectId, model.Keyword,

                                        model.PageIndex, model.PageSize, model.SortOrder);

            var classEntities = await GetAsync<PageResultViewModel<ClassViewModel>>(getClassesBySubjectUrl);

            foreach (var entity in classEntities.Items) entity.Type = entity.Type.Equals(ClassType.Practice.ToString()) ? "Thực hành" : "Lý thuyết";

            return classEntities;
        }

        public async Task<PageResultViewModel<ClientViewModel>> GetStudentsNotExistInClassAsync(string id, ClientRolePageViewModel model)
        {
            var getStudentsNotExistInClassUrl = string.Format(ClassManagementMvcDef.GetStudentsNotExistInClass, ClassManagementMvcDef.ClassApi, id, model.Keyword,

                                                model.PageIndex, model.PageSize, model.SortOrder, model.IsDisabled);

            var studentEntities = await GetAsync<PageResultViewModel<ClientViewModel>>(getStudentsNotExistInClassUrl);

            if (studentEntities is null || studentEntities.Items.Count == 0) return new PageResultViewModel<ClientViewModel> { };

            foreach (var entity in studentEntities.Items)
            {
                if (!string.IsNullOrEmpty(entity.ImagePath)) entity.ImagePath = $"{_configuration["BaseApi"]}{entity.ImagePath}";
            }

            return studentEntities;
        }

        public async Task<object> UpdateClassAsync(string id, UpdateClassViewModel model)
        {
            GetSession();

            var dataContent = model.GetRequestMultipartFormContent(string.Empty);

            var getClassIdUrl = string.Format(ClassManagementMvcDef.GetClassId, ClassManagementMvcDef.ClassApi, id);

            var response = await _httpClient.PutAsync(getClassIdUrl, dataContent);

            if (!response.IsSuccessStatusCode) return response;

            return new Response { };
        }

        public async Task<PageResultViewModel<ClientViewModel>> GetClientsByClassIdAsync(ClientsClassPageViewModel model)
        {
            var getClientsByClassUrl = string.Format(ClassManagementMvcDef.GetClientsByClass, ClassManagementMvcDef.ClassApi, model.ClassId, model.Keyword,

                                        model.PageIndex, model.PageSize, model.SortOrder);

            var clientEntities = await GetAsync<PageResultViewModel<ClientViewModel>>(getClientsByClassUrl);

            if (clientEntities is null || clientEntities.TotalRecords == 0) return new PageResultViewModel<ClientViewModel>();

            foreach (var entity in clientEntities.Items)
            {
                if (!string.IsNullOrEmpty(entity.ImagePath)) entity.ImagePath = $"{_configuration["BaseApi"]}{entity.ImagePath}";
            }

            return clientEntities;
        }
    }
}
