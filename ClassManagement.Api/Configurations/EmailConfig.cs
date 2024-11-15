using ClassManagement.Api.DTO.Sender;

namespace ClassManagement.Api.Configurations
{
    static class EmailConfig
    {
        public static void ConfigureEmail(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailInfo>(configuration.GetSection("EmailSettings"));
        }
    }
}
