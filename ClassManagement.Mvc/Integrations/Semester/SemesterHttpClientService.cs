using ClassManagement.Mvc.Models.Common;
using ClassManagement.Mvc.Models.Page;
using ClassManagement.Mvc.Models.Semester;
using ClassManagement.Mvc.Utilities;

namespace ClassManagement.Mvc.Integrations.Semester
{
    class SemesterHttpClientService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)

                : BaseHttpClientService(configuration, httpClientFactory, httpContextAccessor), ISemestercHttpClientService
    {
        public async Task<object> CreateSemesterAsync(CreateSemesterViewModel model)
        {
            GetSession();

            var response = await _httpClient.PostAsync(ClassManagementMvcDef.SemesterApi, HandleRequest.RequestHandler(model));

            if (!response.IsSuccessStatusCode) return response;

            return new Response();
        }

        public async Task<PageResultViewModel<SemesterViewModel>> GetSemestersAsync(CommonPageViewModel model)
        {
            var getSemesterUrl = string.Format(ClassManagementMvcDef.GetSemesters, ClassManagementMvcDef.SemesterApi, model.Keyword, model.PageIndex,

                                model.PageSize, model.SortOrder);

            var entities = await GetAsync<PageResultViewModel<SemesterViewModel>>(getSemesterUrl);

            if (entities is null || entities.TotalRecords == 0) return new PageResultViewModel<SemesterViewModel> { };

            return entities;
        }

        public async Task<SemesterViewModel> GetByIdAsync(string id)
        {
            var getSemesterIdUrl = string.Format(ClassManagementMvcDef.GetSemesterId, ClassManagementMvcDef.SemesterApi, id);

            var entity = await GetAsync<SemesterViewModel>(getSemesterIdUrl);

            return entity;
        }

        public async Task<object> UpdateSemesterAsync(string id, UpdateSemesterViewModel model)
        {
            GetSession();

            var getSemesterIdUrl = string.Format(ClassManagementMvcDef.GetSemesterId, ClassManagementMvcDef.SemesterApi, id);

            var response = await _httpClient.PutAsync(getSemesterIdUrl, HandleRequest.RequestHandler(model));

            if (!response.IsSuccessStatusCode) return response;

            return new Response();
        }
    }
}
