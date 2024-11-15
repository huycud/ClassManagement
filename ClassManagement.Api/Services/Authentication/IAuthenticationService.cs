using ClassManagement.Api.DTO.Authentication;
using ClassManagement.Api.DTO.Common;

namespace ClassManagement.Api.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<Response> LoginAsync(LoginRequest request);
        Task<bool> LogoutAsync(int id);
        Task<Response> RefreshANewAccessTokenAsync(string refreshToken);
    }
}
