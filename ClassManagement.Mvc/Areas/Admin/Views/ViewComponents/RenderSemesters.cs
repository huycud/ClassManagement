using ClassManagement.Mvc.Integrations.Semester;
using ClassManagement.Mvc.Models.Page;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClassManagement.Mvc.Areas.Admin.Views.ViewComponents
{
    public class RenderSemesters(ISemestercHttpClientService semesterHttpClientService) : ViewComponent
    {
        private readonly ISemestercHttpClientService _semesterHttpClientService = semesterHttpClientService;

        public async Task<IViewComponentResult> InvokeAsync(string? scholasticId)
        {
            var scholastics = await _semesterHttpClientService.GetSemestersAsync(new CommonPageViewModel { PageIndex = 1, PageSize = int.MaxValue });

            return View(scholastics.Items?.Select(x => new SelectListItem
            {
                Text = x.Name,

                Value = x.Id,

                Selected = !string.IsNullOrEmpty(scholasticId) && scholasticId.ToUpper().Equals(x.Id)
            }));
        }
    }
}
