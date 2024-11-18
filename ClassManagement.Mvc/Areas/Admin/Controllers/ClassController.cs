using System.Net;
using System.Reflection;
using ClassManagement.Mvc.Integrations.Class;
using ClassManagement.Mvc.Integrations.Subject;
using ClassManagement.Mvc.Integrations.Users.Manager;
using ClassManagement.Mvc.Models.Class;
using ClassManagement.Mvc.Models.Common;
using ClassManagement.Mvc.Models.Common.Errors;
using ClassManagement.Mvc.Models.Page;
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
    public class ClassController(IClassHttpClientService classHttpClientService, IAdminHttpClientService adminHttpClientService,

                                ISubjectHttpClientService subjectHttpClientService) : Controller
    {
        private readonly IClassHttpClientService _classHttpClientService = classHttpClientService;

        private readonly ISubjectHttpClientService _subjectHttpClientService = subjectHttpClientService;

        private readonly IAdminHttpClientService _adminHttpClientService = adminHttpClientService;

        /// <summary>
        /// Lấy danh sách class với key(nullable) = subjectId
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyword"></param>
        /// <param name="sortProperty"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>Danh sách class</returns>
        [HttpGet]
        public async Task<IActionResult> Index(string? subjectId, string? keyword, SortOrder sortOrder, int pageIndex = 1, int pageSize = 5)
        {
            var model = new ClassPageViewModel
            {
                SubjectId = subjectId,

                Keyword = keyword,

                SortOrder = sortOrder,

                PageIndex = pageIndex,

                PageSize = pageSize
            };

            ViewBag.Keyword = keyword;

            var result = await _classHttpClientService.GetClassesAsync(model);

            var subjects = await _subjectHttpClientService.GetSubjectsAsync(new SubjectPageViewModel { PageIndex = 1, PageSize = int.MaxValue });

            ViewBag.Subjects = subjects.Items?.Select(x => new SelectListItem
            {
                Text = x.Name,

                Value = x.Id,

                Selected = !string.IsNullOrEmpty(model.SubjectId) && model.SubjectId.Equals(x.Id)
            });

            ViewBag.SortOrder = SortOrderList(model);

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetClassById(string id, bool? isClients = false, bool? isHomeworks = false)
        {
            var entity = await _classHttpClientService.GetByIdAsync(id);

            if (entity is null) return RedirectToAction("Forbidden", "Login", new { area = "" });

            return View(entity);
        }

        [HttpGet]
        public IActionResult CreateClass()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClass(CreateClassViewModel model)
        {
            var result = await _classHttpClientService.CreateClassAsync(model);

            if (result is HttpResponseMessage)
            {
                await ModelStateHandler(result, "Tạo lớp");

                return Json(new { isValid = false, html = await Helper.RenderRazorViewToString(this, "_CreateClassPartialView", model) });
            }

            return Json(new { isValid = true, isReload = true });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateClass(string id)
        {
            var entity = await _classHttpClientService.GetByIdAsync(id);

            if (entity is null) return RedirectToAction("Forbidden", "Login", new { area = "" });

            return View(new UpdateClassViewModel
            {
                Id = entity.Id,

                Name = entity.Name,

                ClassSize = entity.ClassSize,

                TeacherId = entity.TeacherItem.Id,

                Subject = entity.Subject,

                Semester = entity.Semester,

                StartedAt = entity.StartedAt,

                EndedAt = entity.EndedAt,

                Type = entity.Type
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateClass(string id, UpdateClassViewModel model)
        {
            var result = await _classHttpClientService.UpdateClassAsync(id, model);

            if (result is HttpResponseMessage)
            {
                await ModelStateHandler(result, "Cập nhật lớp");

                return Json(new { isValid = false, html = await Helper.RenderRazorViewToString(this, "UpdateClass", model) });
            }

            return Json(new
            {
                isValid = true,

                html = await Helper.RenderRazorViewToString(this,

                "_ClassDetailPartialView",

                await _classHttpClientService.GetByIdAsync(id))
            });
        }

        [HttpGet]
        public async Task<IActionResult> AddStudentsToClass(string id, string? keyword, int pageIndex = 1, int pageSize = 5, bool isDisabled = false)
        {
            var entity = await _classHttpClientService.GetByIdAsync(id);

            if (entity is null) return RedirectToAction("Forbidden", "Login", new { area = "" });

            var model = new ClientRolePageViewModel()
            {
                RoleName = RoleConstants.STUDENT_NAME,

                PageIndex = pageIndex,

                PageSize = pageSize,

                Keyword = keyword,

                IsDisabled = isDisabled
            };

            ViewBag.Keyword = keyword;

            ViewBag.SortOrder = SortOrderList(model);

            var clients = await _classHttpClientService.GetStudentsNotExistInClassAsync(id, model);

            var result = new ClassClientsViewModel
            {
                Class = entity,

                Clients = clients
            };

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudentsToClass(string id, List<string> students, string? keyword, int pageIndex = 1, int pageSize = 5, bool isDisabled = false)
        {
            var result = await _classHttpClientService.AddStudentsToClassAsync(id, students);

            if (result is HttpResponseMessage)
            {
                await ModelStateHandler(result, "Thêm vào lớp");

                return Json(new
                {
                    isValid = false,

                    html = await Helper.RenderRazorViewToString(this,

                    "_AddStudentsToClassPartialView",

                    await _classHttpClientService.GetStudentsNotExistInClassAsync(id, new ClientRolePageViewModel
                    {
                        RoleName = RoleConstants.STUDENT_NAME,

                        PageIndex = pageIndex,

                        PageSize = pageSize,

                        Keyword = keyword,

                        IsDisabled = isDisabled
                    }))
                });
            }

            return Json(new { isValid = true, isReload = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetClientsByClassId(string id, string? keyword, SortOrder sortOrder, int pageIndex = 1, int pageSize = 5)
        {
            var entity = await _classHttpClientService.GetByIdAsync(id);

            if (entity is null) return RedirectToAction("Forbidden", "Login", new { area = "" });

            var model = new ClientsClassPageViewModel
            {
                ClassId = entity.Id,

                Keyword = keyword,

                SortOrder = sortOrder,

                PageSize = pageSize,

                PageIndex = pageIndex
            };

            ViewBag.Keyword = keyword;

            ViewBag.SortOrder = SortOrderList(model);

            var clients = await _classHttpClientService.GetClientsByClassIdAsync(model);

            var result = new ClassClientsViewModel
            {
                Class = entity,

                Clients = clients
            };

            return View(result);
        }

        /// <summary>
        /// Hàm xử lí lỗi validate gửi từ server trả về
        /// </summary>
        /// <param name="obj">ClassErrorViewModel</param>
        /// <param name="message"></param>
        /// <returns>Các lỗi validate ứng với từng field</returns>
        private async Task ModelStateHandler(object obj, string message)
        {
            var error = (HttpResponseMessage)obj;

            if (error.StatusCode != HttpStatusCode.OK)
            {
                ClassErrorViewModel viewModel = new();

                var errorInfo = await HandleError<ClassErrorViewModel>.HandleModelState(error, viewModel);

                int count = 0;

                foreach (PropertyInfo field in errorInfo.PropertyInfos)
                {
                    var value = field.GetValue(errorInfo.Value);

                    if (value is not null)
                    {
                        var setEmptyModel = field.Name.Equals("Exist")

                                            || field.Name.Equals("TheoryClass")

                                            || field.Name.Equals("StudentsId")

                                            || field.Name.Equals("Related");

                        if (setEmptyModel) ModelState.AddModelError("", value.ToString());

                        else ModelState.AddModelError(field.Name, value.ToString());
                    }

                    else count++;
                }

                if (count == errorInfo.PropertyInfos.Length) ModelState.AddModelError("", $"{message} thất bại. Vui lòng kiểm tra lại thông tin.");
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
