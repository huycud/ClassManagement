using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Common;

namespace ClassManagement.Mvc.Areas.Admin.Controllers
{
    [Area(AreaConstants.ADMIN_AREA)]
    [Authorize]
    public class HomeController : Controller
    {

        public HomeController() { }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
