using ClassManagement.Mvc.Models.Common;
using ClassManagement.Mvc.Models.Notify;
using ClassManagement.Mvc.Models.Page;
using ClassManagement.Mvc.Utilities;
using Utilities.Common;

namespace ClassManagement.Mvc.Integrations.Notify
{
    class NotifyHttpClientService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)

        : BaseHttpClientService(configuration, httpClientFactory, httpContextAccessor), INotifyHttpClientService
    {
        public async Task<bool> ChangeStatusNotifyAsync(string id, ChangeNotifyStatusViewModel model)
        {
            GetSession();

            var getChangeStatusUrl = string.Format(ClassManagementMvcDef.GetChangeStatus, ClassManagementMvcDef.NotifyApi, id);

            var response = await _httpClient.PutAsync(getChangeStatusUrl, HandleRequest.RequestHandler(model));

            if (!response.IsSuccessStatusCode) return false;

            return true;
        }

        public async Task<object> CreateNotifyAsync(CreateNotifyViewModel model)
        {
            GetSession();

            var dataContent = model.GetRequestMultipartFormContent(model.UserId.ToString());

            var response = await _httpClient.PostAsync(ClassManagementMvcDef.NotifyApi, dataContent);

            if (!response.IsSuccessStatusCode) return response;

            return new Response();
        }

        public async Task<bool> DeleteNotifyAsync(string id)
        {
            GetSession();

            var getNotifyIdUrl = string.Format(ClassManagementMvcDef.GetNotifyId, ClassManagementMvcDef.NotifyApi, id);

            var response = await _httpClient.DeleteAsync(getNotifyIdUrl);

            if (!response.IsSuccessStatusCode) return false;

            return true;
        }

        public async Task<NotifyViewModel> GetByIdAsync(string id)
        {
            var getNotifyIdUrl = string.Format(ClassManagementMvcDef.GetNotifyId, ClassManagementMvcDef.NotifyApi, id);

            var entity = await GetAsync<NotifyViewModel>(getNotifyIdUrl);

            return entity;
        }

        public async Task<PageResultViewModel<NotifyViewModel>> GetNotifiesAsync(NotifyPageViewModel model)
        {
            var getNotifyUrl = string.Format(ClassManagementMvcDef.GetNotifies, ClassManagementMvcDef.NotifyApi, model.UserId, model.Type, model.Keyword,

                                model.PageIndex, model.PageSize, model.SortOrder, model.IsDeleted);

            var entities = await GetAsync<PageResultViewModel<NotifyViewModel>>(getNotifyUrl);

            return entities;
        }

        public async Task<object> UpdateNotifyAsync(string id, UpdateNotifyViewModel model)
        {
            GetSession();

            var dataContent = model.GetRequestMultipartFormContent(string.Empty);

            var getNotifyIdUrl = string.Format(ClassManagementMvcDef.GetNotifyId, ClassManagementMvcDef.NotifyApi, id);

            var response = await _httpClient.PutAsync(getNotifyIdUrl, dataContent);

            if (!response.IsSuccessStatusCode) return response;

            return new Response();
        }
    }
}
