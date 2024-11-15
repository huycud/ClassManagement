using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClassManagement.Mvc.Integrations.Subject;
using ClassManagement.Mvc.Models.Page;

namespace ClassManagement.Mvc.Areas.Admin.Views.ViewComponents
{
    public class RenderSubjects(ISubjectHttpClientService subjectHttpClientService) : ViewComponent
    {
        private readonly ISubjectHttpClientService _subjectHttpClientService = subjectHttpClientService;

        public async Task<IViewComponentResult> InvokeAsync(string? subjectId)
        {
            var subjects = await _subjectHttpClientService.GetSubjectsAsync(new SubjectPageViewModel { PageIndex = 1, PageSize = int.MaxValue });

            return View(subjects.Items?.Select(x => new SelectListItem
            {
                Text = x.Name,

                Value = x.Id,

                Selected = !string.IsNullOrEmpty(subjectId) && subjectId.ToUpper().Equals(x.Id.ToUpper())
            }));
        }
    }
}
