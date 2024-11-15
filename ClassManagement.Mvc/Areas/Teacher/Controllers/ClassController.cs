using ClassManagement.Mvc.Integrations.Subject;
using ClassManagement.Mvc.Integrations.Users.Client;
using ClassManagement.Mvc.Models.Page;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utilities.Common;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Mvc.Areas.Teacher.Controllers
{
    [Area(AreaConstants.TEACHER_AREA)]
    [Authorize(Roles = RoleConstants.ADMIN_NAME)]
    public class ClassController(IClientHttpService clientHttpService, ISubjectHttpClientService subjectHttpClientService) : Controller
    {
        private readonly IClientHttpService _clientHttpService = clientHttpService;

        private readonly ISubjectHttpClientService _subjectHttpClientService = subjectHttpClientService;

        [HttpGet]
        public async Task<IActionResult> Index(int clientId, string? keyword, SortOrder sortOrder, int pageIndex = 1, int pageSize = 5)
        {
            var model = new ClassesClientPageViewModel
            {
                ClientId = clientId,

                Keyword = keyword,

                SortOrder = sortOrder,

                PageIndex = pageIndex,

                PageSize = pageSize
            };

            ViewBag.Keyword = keyword;

            ViewBag.SortOrder = new List<SelectListItem>
            {
                new() { Text = "ID (ASC)", Value = SortOrder.AscendingId.ToString(), Selected = model.SortOrder == SortOrder.AscendingId },

                new() { Text = "ID (DESC)", Value = SortOrder.DescendingId.ToString(), Selected = model.SortOrder == SortOrder.DescendingId },

                new() { Text = "Name (ASC)", Value = SortOrder.AscendingName.ToString(), Selected = model.SortOrder == SortOrder.AscendingName },

                new() { Text = "Name (DESC)", Value = SortOrder.DescendingName.ToString(), Selected = model.SortOrder == SortOrder.DescendingName }
            };

            var result = await _clientHttpService.GetClassesByClientIdAsync(model);

            return View(result);
        }
    }
}
