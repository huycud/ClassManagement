using Microsoft.AspNetCore.Mvc;
using ClassManagement.Mvc.Models.Page;

namespace ClassManagement.Mvc.Areas.Teacher.Views.ViewComponents
{
    public class TeacherPager : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PageViewModel model)
        {
            return Task.FromResult((IViewComponentResult)View("Default", model));
        }
    }
}
