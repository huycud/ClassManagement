using ClassManagement.Mvc.Models.User;

namespace ClassManagement.Mvc.Integrations.Common
{
    public interface IProfileHttpClientService
    {
        Task<object> UpdatePasswordAsync(int id, UpdatePasswordViewModel model);
        Task<object> UpdateAvatarAsync(int id, UpdateImageViewModel model);
    }
}
