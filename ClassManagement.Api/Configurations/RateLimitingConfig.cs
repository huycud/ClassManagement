using System.Threading.RateLimiting;
using Microsoft.AspNetCore.HttpOverrides;
using Utilities.Common;
namespace ClassManagement.Api.Configurations
{
    static class RateLimitingConfig
    {
        public static void ConfigureRateLimiting(IServiceCollection services)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddRateLimiter(options =>
            {
                options.AddPolicy(PolicyConstants.GENERALRATELIMITING, context =>

                    RateLimitPartition.GetFixedWindowLimiter<Func<HttpContext, string>>(

                    partitionKey: context => "general",

                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 100,

                        Window = TimeSpan.FromMinutes(5)
                    }));

                options.AddPolicy(PolicyConstants.IPRATELIMITING, context =>

                    RateLimitPartition.GetFixedWindowLimiter<Func<HttpContext, string>>(

                        partitionKey: context => context.Connection.RemoteIpAddress?.ToString() ?? "Unknown",

                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 100,

                            Window = TimeSpan.FromMinutes(1)
                        }));
            });
        }
    }
}
