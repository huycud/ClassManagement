using Utilities.Common;

namespace ClassManagement.Mvc.Utilities
{
    class SessionMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        private readonly RequestDelegate _next = next;

        private readonly IConfiguration _configuration = configuration;

        public async Task Invoke(HttpContext context)
        {
            var session = context.Session.GetString(SystemConstants.ACCESSTOKEN_NAME);

            if (!string.IsNullOrEmpty(session))
            {
                var userPrincipal = ValidateToken.TokenValidation(session, _configuration);

                if (userPrincipal is null)
                {//gọi tới refresh token
                    context.Session.Remove(SystemConstants.ACCESSTOKEN_NAME);

                    var returnUrl = context.Request.Path + context.Request.QueryString;

                    context.Response.Redirect($"/Account/Login?returnUrl={returnUrl}");

                    return;
                }

                context.User = userPrincipal;
            }

            await _next(context);
        }
    }

    static class SessionMiddlewareExtensions
    {
        public static IApplicationBuilder UseSessionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SessionMiddleware>();
        }
    }
}
