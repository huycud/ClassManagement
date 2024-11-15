using ClassManagement.Api.DTO.Notifies;
using ClassManagement.Api.DTO.Page;

namespace ClassManagement.Api.Services.Notifies
{
    public interface INotifyService
    {
        Task<PageResult<NotifyResponse>> GetAsync(NotifyPageRequest request);
        //Task<List<NotifyResponse>> GetAsync();
        Task<NotifyResponse> GetByIdAsync(string id);
        Task<string> CreateAsync(CreateNotifyRequest request);
        Task<bool> UpdateAsync(string id, UpdateNotifyRequest request);
        Task<bool> ChangeStatusAsync(string id, ChangeNotifyStatusRequest request);
        Task<bool> DeleteAsync(string id);
    }
}
