using ClassManagement.Mvc.Integrations.Common;
using ClassManagement.Mvc.Models.Homework;

namespace ClassManagement.Mvc.Integrations.Homework
{
    public interface IHomeworkHttpClientService : ICommonHttpClientService<HomeworkViewModel, int>
    {
        Task<List<HomeworkViewModel>> GetHomeworksByClassIdAsync(string classId);
    }
}
