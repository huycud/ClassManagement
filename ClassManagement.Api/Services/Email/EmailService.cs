using System.Net;
using System.Net.Mail;
using System.Text;
using ClassManagement.Api.Common.Exceptions;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Sender;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Utilities.Messages;

namespace ClassManagement.Api.Services.Email
{
    public class EmailService(UserManager<User> userManager, IOptions<EmailInfo> emailInfo) : IEmailService
    {
        private readonly EmailInfo _emailInfo = emailInfo.Value;

        private readonly UserManager<User> _userManager = userManager;

        public async Task SendMailAsync(EmailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var smtp = new SmtpClient(_emailInfo.Provider, _emailInfo.Port)
                {
                    Credentials = new NetworkCredential(_emailInfo.From, _emailInfo.Password),

                    EnableSsl = true,

                    UseDefaultCredentials = false
                };

                var mailMessage = new MailMessage()
                {
                    From = new MailAddress(_emailInfo.From, _emailInfo.DisplayName),

                    Subject = request.Subject,

                    Body = request.Content,

                    IsBodyHtml = true
                };

                mailMessage.To.Add(request.To);

                await smtp.SendMailAsync(mailMessage, cancellationToken);

                mailMessage.Dispose();
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.ToString());
            }
        }

        public async Task<bool> ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            var entity = await _userManager.FindByEmailAsync(request.Email)

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Email"));

            if (entity.EmailConfirmed) throw new BadRequestException(string.Format(ErrorMessages.WAS_CONFIRMED, "Email"));

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.ConfirmEmailToken));

            var confirmEmailResult = await _userManager.ConfirmEmailAsync(entity, decodedToken);

            if (!confirmEmailResult.Succeeded) throw new BadRequestException(string.Format(ErrorMessages.WAS_EXPIRED, "Token"));

            return true;
        }
    }
}
