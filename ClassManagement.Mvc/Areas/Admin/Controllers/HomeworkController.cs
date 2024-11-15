using Microsoft.AspNetCore.Mvc;
using ClassManagement.Mvc.Integrations.Homework;

namespace ClassManagement.Mvc.Areas.Admin.Controllers
{
    public class HomeworkController(IHomeworkHttpClientService homeworkHttpClientService) : Controller
    {
        private readonly IHomeworkHttpClientService _homeworkHttpClientService = homeworkHttpClientService;

        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> GetHomeworksByClassIdPartialView(string id)
        //{
        //    var entity = await _homeworkHttpClientService.GetByIdAsync(id);

        //    if (entity is null) return RedirectToAction("Forbidden", "Login", new { area = "" });

        //    return PartialView("~/Areas/Admin/Views/Homework/_GetHomeworksByClassIdPartialView.cshtml", entity);
        //}
    }
}
