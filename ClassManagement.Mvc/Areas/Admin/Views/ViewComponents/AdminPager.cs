using Microsoft.AspNetCore.Mvc;
using ClassManagement.Mvc.Models.Page;

namespace ClassManagement.Mvc.Areas.Admin.Views.ViewComponents
{
    public class AdminPager : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PageViewModel model)
        {
            return Task.FromResult((IViewComponentResult)View("Default", model));
        }
    }
}
