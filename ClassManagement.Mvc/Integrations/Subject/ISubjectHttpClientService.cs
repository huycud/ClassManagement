using ClassManagement.Mvc.Integrations.Common;
using ClassManagement.Mvc.Models.Page;
using ClassManagement.Mvc.Models.Subject;

namespace ClassManagement.Mvc.Integrations.Subject
{
    public interface ISubjectHttpClientService : ICommonHttpClientService<SubjectViewModel, string>
    {
        Task<PageResultViewModel<SubjectViewModel>> GetSubjectsAsync(SubjectPageViewModel model);
        Task<object> CreateSubjectAsync(CreateSubjectViewModel model);
        Task<object> UpdateSubjectAsync(string id, UpdateSubjectViewModel model);
    }
}
