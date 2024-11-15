using ClassManagement.Mvc.Integrations.Common;
using ClassManagement.Mvc.Models.Page;
using ClassManagement.Mvc.Models.Semester;

namespace ClassManagement.Mvc.Integrations.Semester 
{
    public interface ISemestercHttpClientService : ICommonHttpClientService<SemesterViewModel, string>
    {
        Task<PageResultViewModel<SemesterViewModel>> GetSemestersAsync(CommonPageViewModel model);
        Task<object> CreateSemesterAsync(CreateSemesterViewModel model);
        Task<object> UpdateSemesterAsync(string id, UpdateSemesterViewModel model);
    }
}
