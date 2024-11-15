using System.Net;
using System.Text.Json;

namespace ClassManagement.Api.Common.Exceptions
{
    class ExceptionHandler(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception error)
            {
                var response = context.Response;

                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    BadRequestException e => (int)HttpStatusCode.BadRequest,

                    KeyNotFoundException e => (int)HttpStatusCode.NotFound,

                    ForbiddenException e => (int)HttpStatusCode.Forbidden,

                    _ => (int)HttpStatusCode.InternalServerError,
                };

                var result = JsonSerializer.Serialize(new { message = error?.Message });

                await response.WriteAsync(result);
            }
        }
    }
}
