using System.Threading.RateLimiting;
using Microsoft.AspNetCore.HttpOverrides;
using Utilities.Common;
using Utilities.Messages;
namespace ClassManagement.Api.Configurations
{
    static class RateLimiterConfig
    {
        public static void ConfigureRateLimiter(IServiceCollection services)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddRateLimiter(options =>
            {
                options.OnRejected = async (context, cancellationToken) =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();

                    logger.LogWarning("Rate limit exceeded for {Path} by {IP}",

                        context.HttpContext.Request.Path,

                        context.HttpContext.Connection.RemoteIpAddress?.ToString());

                    context.HttpContext.Response.ContentType = "application/json";

                    await context.HttpContext.Response.WriteAsync(ErrorMessages.EXCEEDEDRATELIMITING, cancellationToken);
                };

                options.AddPolicy(PolicyConstants.GENERALRATELIMITING, context =>

                    RateLimitPartition.GetSlidingWindowLimiter<Func<HttpContext, string>>(

                    partitionKey: context => "general",

                    factory: _ => new SlidingWindowRateLimiterOptions
                    {
                        PermitLimit = 100,

                        Window = TimeSpan.FromMinutes(10),

                        SegmentsPerWindow = 4
                    }))

                    .RejectionStatusCode = 429;

                options.AddPolicy(PolicyConstants.IPRATELIMITING, context =>

                    RateLimitPartition.GetFixedWindowLimiter<Func<HttpContext, string>>(

                        partitionKey: context => context.Connection.RemoteIpAddress?.ToString() ?? "Unknown",

                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 100,

                            Window = TimeSpan.FromMinutes(1)
                        }))

                    .RejectionStatusCode = 429;
            });
        }
    }
}
