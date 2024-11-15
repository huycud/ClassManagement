using ClassManagement.Mvc.Models.Common;
using ClassManagement.Mvc.Models.User;
using ClassManagement.Mvc.Utilities;

namespace ClassManagement.Mvc.Integrations
{
    class UserHttpClientService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)

                        : BaseHttpClientService(configuration, httpClientFactory, httpContextAccessor)
    {
        protected async Task<object> UpdatePasswordAsync(string url, UpdatePasswordViewModel model)
        {
            GetSession();

            var response = await _httpClient.PutAsync(url, HandleRequest.RequestHandler(model));

            if (!response.IsSuccessStatusCode) return response;

            return new Response();
        }

        protected async Task<object> UpdateImageAsync(string url, UpdateImageViewModel model)
        {
            GetSession();

            var dataContent = model.GetRequestMultipartFormContent(string.Empty);

            var response = await _httpClient.PutAsync(url, dataContent);

            if (!response.IsSuccessStatusCode) return response;

            return new Response();
        }
    }
}
