using Microsoft.EntityFrameworkCore;
using ClassManagement.Api.Data.EF;
using ClassManagement.Api.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Utilities.Common;

namespace ClassManagement.Api.Configurations
{
    static class DatabaseConfig
    {
        public static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(
                options =>
                    options.UseSqlServer(configuration.GetConnectionString(SystemConstants.CONNECTIONSTRING))
                               .UseLazyLoadingProxies()
            );
        }

        public static void ConfigureDeclareDb(IServiceProvider service)
        {
            using (var scope = service.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated();
            }
        }

        public static void ConfigureContext(IServiceCollection services)
        {
            services.AddIdentityCore<User>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
