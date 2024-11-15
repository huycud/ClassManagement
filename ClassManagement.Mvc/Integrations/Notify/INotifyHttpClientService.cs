using ClassManagement.Mvc.Integrations.Common;
using ClassManagement.Mvc.Models.Notify;
using ClassManagement.Mvc.Models.Page;

namespace ClassManagement.Mvc.Integrations.Notify
{
    public interface INotifyHttpClientService : ICommonHttpClientService<NotifyViewModel, string>
    {
        Task<PageResultViewModel<NotifyViewModel>> GetNotifiesAsync(NotifyPageViewModel model);
        Task<object> CreateNotifyAsync(CreateNotifyViewModel model);
        Task<object> UpdateNotifyAsync(string id, UpdateNotifyViewModel model);
        Task<bool> ChangeStatusNotifyAsync(string id, ChangeNotifyStatusViewModel model);
        Task<bool> DeleteNotifyAsync(string id);
    }
}
