using Microsoft.AspNetCore.Authentication.Cookies;

namespace ClassManagement.Mvc.Configurations
{
    internal static class AuthenConfig
    {
        public static void ConfigureAuthen(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)

                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Login";

                        options.AccessDeniedPath = "/Login/Forbidden/";
                    });

            //services.AddStackExchangeRedisCache(action =>
            //{
            //    action.InstanceName = "SchoolMng";

            //    action.Configuration = "127.0.0.1:6379";
            //});

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);

                options.Cookie.IsEssential = true;

                options.Cookie.HttpOnly = true;

                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

                options.Cookie.SameSite = SameSiteMode.None;
            });
        }
    }
}
