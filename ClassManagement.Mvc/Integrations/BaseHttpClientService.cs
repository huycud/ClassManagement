using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Utilities.Common;

namespace ClassManagement.Mvc.Integrations
{
    class BaseHttpClientService
    {
        protected readonly HttpClient _httpClient;

        protected readonly IConfiguration _configuration;

        protected readonly IHttpClientFactory _httpClientFactory;

        protected readonly IHttpContextAccessor _httpContextAccessor;

        protected ISession _session => _httpContextAccessor.HttpContext.Session;

        public BaseHttpClientService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;

            _httpClientFactory = httpClientFactory;

            _httpContextAccessor = httpContextAccessor;

            _httpClient = _httpClientFactory.CreateClient();

            _httpClient.BaseAddress = new Uri($"{_configuration["Host:BaseApi"]}");
        }

        protected async Task<T> GetAsync<T>(string url)
        {
            GetSession();

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode) return default;

            var content = await response.Content.ReadAsStringAsync();

            var format = SystemConstants.FORMAT_STRING;

            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };

            var data = JsonConvert.DeserializeObject<T>(content, dateTimeConverter);

            return data;
        }

        protected void GetSession()
        {
            var sessions = _session.GetString(SystemConstants.ACCESSTOKEN_NAME);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(SystemConstants.SCHEME_NAME, sessions);
        }
    }
}
