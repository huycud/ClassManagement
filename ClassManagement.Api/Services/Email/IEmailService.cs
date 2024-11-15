using ClassManagement.Api.DTO.Sender;

namespace ClassManagement.Api.Services.Email
{
    public interface IEmailService
    {
        Task SendMailAsync(EmailRequest request, CancellationToken cancellationToken);
        Task<bool> ConfirmEmailAsync(ConfirmEmailRequest request);
    }
}