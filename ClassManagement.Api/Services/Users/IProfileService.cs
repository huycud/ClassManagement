using ClassManagement.Api.DTO.Authentication;
using ClassManagement.Api.DTO.Users;

namespace ClassManagement.Api.Services.Users
{
    public interface IProfileService
    {
        Task<bool> UpdatePasswordAsync(int id, UpdatePasswordRequest request);
        Task<bool> ForgotPasswordAsync(ForgotPasswordRequest request, CancellationToken cancellationToken);
        Task<bool> ResetPasswordAsync(ResetPasswordRequest request);
        Task<bool> UpdateImageAsync(int id, UpdateImageRequest request, CancellationToken cancellationToken);
    }
}
