using ClassManagement.Mvc.Models.Authentication;

namespace ClassManagement.Mvc.Integrations.Authenticate
{
    public interface IAuthHttpClientService
    {
        Task<object> LoginAsync(LoginViewModel model);
        Task<object> LogoutAsync(int id);
        Task<object> RefreshTokenAsync();
    }
}
