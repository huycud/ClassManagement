using Microsoft.AspNetCore.Mvc;
using ClassManagement.Mvc.Integrations.Homework;

namespace ClassManagement.Mvc.Areas.Admin.Views.ViewComponents
{
    public class GetHomeworksByClassId : ViewComponent
    {
        private readonly IHomeworkHttpClientService _homeworkHttpClientService;

        public GetHomeworksByClassId(IHomeworkHttpClientService homeworkHttpClientService)
        {
            _homeworkHttpClientService = homeworkHttpClientService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string classId)
        {
            var homeworks = await _homeworkHttpClientService.GetHomeworksByClassIdAsync(classId);

            return View(homeworks);
        }
    }
}
