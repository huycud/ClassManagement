using ClassManagement.Mvc.Models.Authentication;
using ClassManagement.Mvc.Models.Common;
using ClassManagement.Mvc.Utilities;
using Newtonsoft.Json;

namespace ClassManagement.Mvc.Integrations.Authenticate
{
    public class AuthHttpClientService : IAuthHttpClientService
    {
        private readonly HttpClient _httpClient;

        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthHttpClientService(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            _httpClient = httpClient;

            _configuration = configuration;

            _httpClient.BaseAddress = new Uri($"{_configuration["Host:BaseApi"]}");
        }

        public async Task<object> LoginAsync(LoginViewModel model)
        {
            var loginUrl = string.Format(ClassManagementMvcDef.LoginApi, ClassManagementMvcDef.AuthenticationApi);

            var response = await _httpClient.PostAsync(loginUrl, HandleRequest.RequestHandler(model));

            if (!response.IsSuccessStatusCode) return response;

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Response>(content);
        }

        public async Task<object> LogoutAsync(int id)
        {
            var logoutUrl = string.Format(ClassManagementMvcDef.LogoutApi, ClassManagementMvcDef.AuthenticationApi);

            var response = await _httpClient.PostAsync(logoutUrl, HandleRequest.RequestHandler(id));

            if (!response.IsSuccessStatusCode) return response;

            return new Response { };
        }

        public async Task<object> RefreshTokenAsync()
        {
            throw new NotImplementedException();
        }
    }
}
