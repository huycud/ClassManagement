using System.Security.Claims;
using ClassManagement.Mvc.Integrations.Users.Client;
using ClassManagement.Mvc.Models.Page;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utilities.Common;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Mvc.Areas.Teacher.Controllers
{
    [Area(AreaConstants.TEACHER_AREA)]
    [Authorize(Policy = "RequireAllRoles")]
    public class HomeController(IClientHttpService clientHttpService) : Controller
    {
        private readonly IClientHttpService _clientHttpService = clientHttpService;

        [HttpGet]
        public async Task<IActionResult> Index(string? keyword, SortOrder sortOrder, int pageIndex = 1, int pageSize = 5)
        {
            _ = Int32.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out int id);

            var entity = await _clientHttpService.GetByIdAsync(id);

            if (entity is null) return RedirectToAction("Index", "Login", new { area = "" });

            var model = new ClassesClientPageViewModel
            {
                ClientId = entity.Id,

                Keyword = keyword,

                SortOrder = sortOrder,

                PageIndex = pageIndex,

                PageSize = pageSize
            };

            ViewBag.Keyword = keyword;

            var result = await _clientHttpService.GetClassesByClientIdAsync(model);

            ViewBag.SortOrder = new List<SelectListItem>
            {
                new() { Text = "ID (ASC)", Value = SortOrder.AscendingId.ToString(), Selected = model.SortOrder == SortOrder.AscendingId },

                new() { Text = "ID (DESC)", Value = SortOrder.DescendingId.ToString(), Selected = model.SortOrder == SortOrder.DescendingId },

                new() { Text = "Name (ASC)", Value = SortOrder.AscendingName.ToString(), Selected = model.SortOrder == SortOrder.AscendingName },

                new() { Text = "Name (DESC)", Value = SortOrder.DescendingName.ToString(), Selected = model.SortOrder == SortOrder.DescendingName }
            };

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Login", new { area = "" });
        }
    }
}
