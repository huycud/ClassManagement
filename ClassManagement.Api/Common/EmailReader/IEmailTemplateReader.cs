
namespace ClassManagement.Api.Common.EmailReader
{
    public interface IEmailTemplateReader
    {
        Task<string> GetTemplateAsync(string templateName);
    }
}