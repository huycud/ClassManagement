using System.Net;
using System.Reflection;
using ClassManagement.Mvc.Integrations.Semester;
using ClassManagement.Mvc.Models.Common.Errors;
using ClassManagement.Mvc.Models.Page;
using ClassManagement.Mvc.Models.Semester;
using ClassManagement.Mvc.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utilities.Common;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Mvc.Areas.Admin.Controllers
{
    [Area(AreaConstants.ADMIN_AREA)]
    [Authorize]
    public class SemesterController(ISemestercHttpClientService semesterHttpClientService) : Controller
    {
        private readonly ISemestercHttpClientService _semesterHttpClientService = semesterHttpClientService;

        [HttpGet]
        public async Task<IActionResult> Index(string? keyword, SortOrder sortOrder, int pageIndex = 1, int pageSize = 5)
        {
            var model = new CommonPageViewModel
            {
                PageIndex = pageIndex,

                PageSize = pageSize,

                SortOrder = sortOrder,

                Keyword = keyword
            };

            ViewBag.Keyword = keyword;

            ViewBag.SortOrder = SortOrderList(model);

            var result = await _semesterHttpClientService.GetSemestersAsync(model);

            return View(result);
        }

        [HttpGet]
        public IActionResult CreateScholastic()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateScholastic(CreateSemesterViewModel model)
        {
            var result = await _semesterHttpClientService.CreateSemesterAsync(model);

            if (result is HttpResponseMessage)
            {
                await ModelStateHandler(result, "Tạo");

                return Json(new { isValid = false, html = await Helper.RenderRazorViewToString(this, "CreateSemester", model) });
            }

            return Json(new { isValid = true, isReload = true });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateScholastic(string id)
        {
            var entity = await _semesterHttpClientService.GetByIdAsync(id);

            if (entity is null) return RedirectToAction("Forbidden", "Login", new { area = "" });

            return View(new UpdateSemesterViewModel { Id = entity.Id, Name = entity.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateScholastic(string id, UpdateSemesterViewModel model)
        {
            var result = await _semesterHttpClientService.UpdateSemesterAsync(id, model);

            if (result is HttpResponseMessage)
            {
                await ModelStateHandler(result, "Cập nhật");

                return Json(new { isValid = false, html = await Helper.RenderRazorViewToString(this, "UpdateSemester", model) });
            }

            return Json(new
            {
                isValid = true,

                html = await Helper.RenderRazorViewToString(this,

                "_SemesterDetailPartialView",

                await _semesterHttpClientService.GetByIdAsync(id))
            });
        }

        /// <summary>
        /// Hàm xử lí lỗi validate gửi từ server trả về
        /// </summary>
        /// <param name="obj">SemsterErrorViewModel</param>
        /// <param name="message"></param>
        /// <returns>Các lỗi validate ứng với từng field</returns>
        private async Task ModelStateHandler(object obj, string message)
        {
            var error = (HttpResponseMessage)obj;

            if (error.StatusCode != HttpStatusCode.OK)
            {
                SemesterErrorViewModel viewModel = new();

                var errorInfo = await HandleError<SemesterErrorViewModel>.HandleModelState(error, viewModel);

                int count = 0;

                foreach (PropertyInfo field in errorInfo.PropertyInfos)
                {
                    var value = field.GetValue(errorInfo.Value);

                    if (value is not null) ModelState.AddModelError(field.Name, value.ToString());

                    else count++;
                }

                if (count == errorInfo.PropertyInfos.Length) ModelState.AddModelError("", $"{message} fail. Please check information again");
            }
        }

        private List<SelectListItem> SortOrderList(CommonPageViewModel model)
        {
            return ViewBag.SortOrder = new List<SelectListItem>
            {
                new() { Text = "ID (ASC)", Value = SortOrder.AscendingId.ToString(), Selected = model.SortOrder == SortOrder.AscendingId },

                new() { Text = "ID (DESC)", Value = SortOrder.DescendingId.ToString(), Selected = model.SortOrder == SortOrder.DescendingId },

                new() { Text = "Name (ASC)", Value = SortOrder.AscendingName.ToString(), Selected = model.SortOrder == SortOrder.AscendingName },

                new() { Text = "Name (DESC)", Value = SortOrder.DescendingName.ToString(), Selected = model.SortOrder == SortOrder.DescendingName }
            };
        }
    }
}
