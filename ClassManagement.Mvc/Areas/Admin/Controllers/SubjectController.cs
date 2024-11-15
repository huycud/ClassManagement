using System.Net;
using System.Reflection;
using ClassManagement.Mvc.Integrations.Class;
using ClassManagement.Mvc.Integrations.Department;
using ClassManagement.Mvc.Integrations.Subject;
using ClassManagement.Mvc.Models.Common.Errors;
using ClassManagement.Mvc.Models.Page;
using ClassManagement.Mvc.Models.Subject;
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
    public class SubjectController(ISubjectHttpClientService subjectHttpClientService, IClassHttpClientService classHttpClientService,

                                    IDepartmentHttpClientService departmentHttpClientService) : Controller
    {
        private readonly ISubjectHttpClientService _subjectHttpClientService = subjectHttpClientService;

        private readonly IDepartmentHttpClientService _departmentHttpClientService = departmentHttpClientService;

        private readonly IClassHttpClientService _classHttpClientService = classHttpClientService;

        [HttpGet]
        public async Task<IActionResult> Index(string? departmentId, string keyword, SortOrder sortOrder, int pageIndex = 1, int pageSize = 5)
        {
            var model = new SubjectPageViewModel
            {
                DepartmentId = departmentId,

                PageIndex = pageIndex,

                PageSize = pageSize,

                SortOrder = sortOrder,

                Keyword = keyword
            };

            ViewBag.Keyword = keyword;

            var departments = await _departmentHttpClientService.GetDepartmentAsync(new CommonPageViewModel { PageIndex = 1, PageSize = int.MaxValue });

            ViewBag.Departments = departments.Items?.Select(x => new SelectListItem
            {
                Text = x.Name,

                Value = x.Id,

                Selected = !string.IsNullOrEmpty(model.DepartmentId) && model.DepartmentId.ToUpper().Equals(x.Id.ToUpper())
            });

            ViewBag.SortOrder = SortOrderList(model);

            var result = await _subjectHttpClientService.GetSubjectsAsync(model);

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetSubjectById(string id)
        {
            var entity = await _subjectHttpClientService.GetByIdAsync(id);

            if (entity is null) return RedirectToAction("Index", "Login", new { area = "" });

            return View(entity);
        }

        [HttpGet]
        public IActionResult CreateSubject()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubject(CreateSubjectViewModel model)
        {
            var result = await _subjectHttpClientService.CreateSubjectAsync(model);

            if (result is HttpResponseMessage)
            {
                await ModelStateHandler(result, "Tạo mới");

                return Json(new { isValid = false, html = await Helper.RenderRazorViewToString(this, "_CreateSubjectPartialView", model) });
            }

            return Json(new { isValid = true, isReload = true });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSubject(string id)
        {
            var entity = await _subjectHttpClientService.GetByIdAsync(id);

            if (entity is null) return RedirectToAction("Forbidden", "Login", new { area = "" });

            //var test = Enum.TryParse(entity.Status, out Status status) ? status : default;

            return View(new UpdateSubjectViewModel
            {
                Id = entity.Id,

                Name = entity.Name,

                Credit = entity.Credit,

                Status = entity.Status,

                ClassesId = entity.ClassesId,

                DepartmentId = entity.DepartmentId,

                IsPracticed = entity.IsPracticed
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSubject(string id, UpdateSubjectViewModel model)
        {
            var result = await _subjectHttpClientService.UpdateSubjectAsync(id, model);

            if (result is HttpResponseMessage)
            {
                await ModelStateHandler(result, "Cập nhật");

                return Json(new { isValid = false, html = await Helper.RenderRazorViewToString(this, "UpdateSubject", model) });
            }

            var entity = await _subjectHttpClientService.GetByIdAsync(id);

            return Json(new { isValid = true, html = await Helper.RenderRazorViewToString(this, "_SubjectDetailPartialView", entity) });
        }

        /// <summary>
        /// Hàm xử lí lỗi validate gửi từ server trả về
        /// </summary>
        /// <param name="obj">SubjectErrorViewModel</param>
        /// <param name="message"></param>
        /// <returns>Các lỗi validate ứng với từng field</returns>
        private async Task ModelStateHandler(object obj, string message)
        {
            var error = (HttpResponseMessage)obj;

            if (error.StatusCode != HttpStatusCode.OK)
            {
                SubjectErrorViewModel viewModel = new();

                var errorInfo = await HandleError<SubjectErrorViewModel>.HandleModelState(error, viewModel);

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
