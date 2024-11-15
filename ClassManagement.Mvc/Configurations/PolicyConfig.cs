using Utilities.Common;

namespace ClassManagement.Mvc.Configurations
{
    internal static class PolicyConfig
    {
        public static void ConfigurePolicy(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: PolicyConstants.CORS_NAME,

                    policy =>
                    {
                        policy.WithOrigins("https://localhost:7036").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                    });
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAllRoles", policy => policy.RequireRole(RoleConstants.ADMIN_NAME, RoleConstants.TEACHER_NAME, RoleConstants.STUDENT_NAME));

                options.AddPolicy("RequireLowerRoles", policy => policy.RequireRole(RoleConstants.ADMIN_NAME, RoleConstants.TEACHER_NAME));

                options.AddPolicy("RequiredLowestRoles", policy => policy.RequireRole(RoleConstants.TEACHER_NAME, RoleConstants.STUDENT_NAME));
            });


            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => false;

            //    options.MinimumSameSitePolicy = SameSiteMode.None;

            //    options.Secure = CookieSecurePolicy.Always;
            //});
        }
    }
}
