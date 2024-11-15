using Microsoft.AspNetCore.Mvc;

namespace ClassManagement.Mvc.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
