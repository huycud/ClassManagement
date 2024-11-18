using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Utilities.Common;

namespace ClassManagement.Api.Configurations
{
    static class IdentityConfig
    {
        public static void ConfigureIdentity(IServiceCollection services, IConfiguration configuration)
        {
            _ = int.TryParse(configuration.GetSection("IdentityConfig:PasswordRequiredUniqueChars").Value, out int minChars);

            _ = int.TryParse(configuration.GetSection("IdentityConfig:LockoutMaxFailedAccessAttempts").Value, out int accessAttempts);

            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;

                options.Password.RequireDigit = false;

                options.Password.RequireLowercase = false;

                options.Password.RequireNonAlphanumeric = false;

                options.Password.RequireUppercase = false;

                options.Password.RequiredUniqueChars = minChars;

                options.Lockout.MaxFailedAccessAttempts = accessAttempts;

                options.User.RequireUniqueEmail = true;
            });

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(1);
            });
        }

        public static void ConfigureAuthenticate(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;

                    opt.SaveToken = true;

                    opt.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception is SecurityTokenExpiredException)
                            {
                                Log.Warning("JWT Token expired: {Message}", context.Exception.Message);
                            }

                            else Log.Error("Authentication failed: {Message}", context.Exception.Message);

                            return Task.CompletedTask;
                        }
                    };

                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration.GetSection("Jwt:Issuer").Value,

                        ValidAudience = configuration.GetSection("Jwt:Audience").Value,

                        //ValidateIssuerSigningKey = true,

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:SecretKey").Value)),

                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(PolicyConstants.REQUIREALLROLES, policy => policy.RequireRole(RoleConstants.ADMIN_NAME, RoleConstants.TEACHER_NAME, RoleConstants.STUDENT_NAME));

                opt.AddPolicy(PolicyConstants.REQUIRELOWERROLES, policy => policy.RequireRole(RoleConstants.ADMIN_NAME, RoleConstants.TEACHER_NAME));

                opt.AddPolicy(PolicyConstants.REQUIRELOWESTROLES, policy => policy.RequireRole(RoleConstants.TEACHER_NAME, RoleConstants.STUDENT_NAME));
            });
        }
    }
}
