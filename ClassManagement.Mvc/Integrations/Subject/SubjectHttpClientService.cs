using ClassManagement.Mvc.Models.Common;
using ClassManagement.Mvc.Models.Page;
using ClassManagement.Mvc.Models.Subject;
using ClassManagement.Mvc.Utilities;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Mvc.Integrations.Subject
{
    class SubjectHttpClientService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)

        : BaseHttpClientService(configuration, httpClientFactory, httpContextAccessor), ISubjectHttpClientService
    {
        public async Task<object> CreateSubjectAsync(CreateSubjectViewModel model)
        {
            GetSession();

            var dataContent = model.GetRequestMultipartFormContent(string.Empty);

            var response = await _httpClient.PostAsync(ClassManagementMvcDef.SubjectApi, dataContent);

            if (!response.IsSuccessStatusCode) return response;

            return new Response { };
        }

        public async Task<SubjectViewModel> GetByIdAsync(string id)
        {
            var getSubjectIdUrl = string.Format(ClassManagementMvcDef.GetSubjectId, ClassManagementMvcDef.SubjectApi, id);

            var entity = await GetAsync<SubjectViewModel>(getSubjectIdUrl);

            if (entity is null) return new SubjectViewModel { };

            entity.IsPracticed = entity.IsPracticed.Equals("True") ? "Có thực hành" : "Lý thuyết";

            entity.Status = entity.Status.Equals(Status.Opening.ToString()) ? "Đang mở" : "Đã đóng";

            return entity;
        }

        public async Task<PageResultViewModel<SubjectViewModel>> GetSubjectsAsync(SubjectPageViewModel model)
        {
            var getSubjectUrl = string.Format(ClassManagementMvcDef.GetSubjects, ClassManagementMvcDef.SubjectApi, model.DepartmentId, model.Keyword,

                                model.PageIndex, model.PageSize, model.SortOrder);

            var entities = await GetAsync<PageResultViewModel<SubjectViewModel>>(getSubjectUrl);

            if (entities is null || entities.TotalRecords == 0) return new PageResultViewModel<SubjectViewModel> { };

            foreach (var item in entities.Items)
            {
                item.Status = item.Status.Equals(Status.Opening.ToString()) ? "Đang mở" : "Đã đóng";

                item.IsPracticed = item.IsPracticed.Equals("True") ? "Có thực hành" : "Lý thuyết";
            }

            return entities;
        }

        public async Task<object> UpdateSubjectAsync(string id, UpdateSubjectViewModel model)
        {
            GetSession();

            model.IsPracticed = model.IsPracticed.Equals("Có thực hành") ? "True" : "False";

            var dataContent = model.GetRequestMultipartFormContent(string.Empty);

            var getSubjectIdUrl = string.Format(ClassManagementMvcDef.GetSubjectId, ClassManagementMvcDef.SubjectApi, id);

            var response = await _httpClient.PutAsync(getSubjectIdUrl, dataContent);

            if (!response.IsSuccessStatusCode) return response;

            return new Response { };
        }
    }
}
