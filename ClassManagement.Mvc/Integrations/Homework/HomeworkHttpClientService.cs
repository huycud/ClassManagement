using ClassManagement.Api.Data.Entities;
using ClassManagement.Mvc.Models.Homework;

namespace ClassManagement.Mvc.Integrations.Homework
{
    internal class HomeworkHttpClientService : BaseHttpClientService, IHomeworkHttpClientService
    {
        public HomeworkHttpClientService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)

            : base(configuration, httpClientFactory, httpContextAccessor) { }

        public async Task<HomeworkViewModel> GetByIdAsync(int id)
        {
            var entity = await GetAsync<HomeworkViewModel>($"api/homeworks/{id}");

            return entity;
        }

        public async Task<List<HomeworkViewModel>> GetHomeworksByClassIdAsync(string classId)
        {
            var entities = await GetAsync<List<HomeworkViewModel>>($"api/homeworks/get-homeworks-by-class-id/{classId}");

            return entities;
        }
    }
}
