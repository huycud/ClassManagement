using System.Net;
using System.Reflection;
using ClassManagement.Mvc.Integrations.Role;
using ClassManagement.Mvc.Integrations.Subject;
using ClassManagement.Mvc.Integrations.Users.Client;
using ClassManagement.Mvc.Integrations.Users.Manager;
using ClassManagement.Mvc.Models.Clients;
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
    [Authorize(Roles = RoleConstants.ADMIN_NAME)]
    public class AdminController(IAdminHttpClientService adminHttpClientService, ISubjectHttpClientService subjectHttpClientService,

                                IRoleHttpClientService roleHttpClientService, IClientHttpService clientHttpService) : Controller
    {
        private readonly IAdminHttpClientService _adminHttpClientService = adminHttpClientService;

        private readonly IRoleHttpClientService _roleHttpClientService = roleHttpClientService;

        private readonly ISubjectHttpClientService _subjectHttpClientService = subjectHttpClientService;

        private readonly IClientHttpService _clientHttpService = clientHttpService;

        [HttpGet]
        public async Task<IActionResult> Index(string? keyword, string? roleName, SortOrder sortOrder, int pageIndex = 1, int pageSize = 5, bool isDisabled = false)
        {
            var model = new ClientRolePageViewModel()
            {
                RoleName = roleName,

                PageIndex = pageIndex,

                PageSize = pageSize,

                SortOrder = sortOrder,

                Keyword = keyword,

                IsDisabled = isDisabled
            };

            ViewBag.Keyword = keyword;

            var result = await _adminHttpClientService.GetClientsByRoleNameAsync(model);

            var roles = await _roleHttpClientService.GetAsync(new CommonPageViewModel { PageIndex = 1, PageSize = int.MaxValue });

            ViewBag.Role = roles.Items?.Select(x => new SelectListItem()
            {
                Text = x.Name,

                Value = x.Name,

                Selected = !string.IsNullOrEmpty(model.RoleName) && model.RoleName.ToUpper().Equals(x.Name)
            });

            ViewBag.SortOrder = SortOrderList(model);

            return View(result);
        }

        /// <summary> ???
        /// Lấy thông tin người dùng bằng id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Thông tin chi tiết của người dùng</returns>
        //[HttpGet]
        //public async Task<IActionResult> GetClientById(int id, string? keyword, SortOrder sortOrder, int pageIndex = 1, int pageSize = 5)
        //{
        //    var entity = await _adminHttpClientService.GetClientByIdAsync(id.ToString());

        //    if (entity is null) return RedirectToAction("Forbidden", "Login", new { area = "" });

        //    var model = new ClassesClientPageViewModel
        //    {
        //        ClientId = id,

        //        Keyword = keyword,

        //        SortOrder = sortOrder,

        //        PageIndex = pageIndex,

        //        PageSize = pageSize
        //    };

        //    ViewBag.Keyword = keyword;

        //    var entityRoles = await _roleHttpClientService.GetRolesByClientId(id.ToString());

        //    foreach (var role in entityRoles.Roles)
        //    {
        //        if (role.Equals(RoleName.TEACHER) || role.Equals(RoleName.STUDENT))
        //        {
        //            ViewBag.Type = role;

        //            break;
        //        }
        //    }

        //    var classes = await _clientHttpService.GetClassesByClientIdAsync(model);

        //    ViewBag.SortOrder = new List<SelectListItem>
        //    {
        //        new() { Text = "ID (ASC)", Value = SortOrder.ID_ASC.ToString(), Selected = model.SortOrder == SortOrder.ID_ASC },

        //        new() { Text = "ID (DESC)", Value = SortOrder.ID_DESC.ToString(), Selected = model.SortOrder == SortOrder.ID_DESC },

        //        new() { Text = "Name (ASC)", Value = SortOrder.NAME_ASC.ToString(), Selected = model.SortOrder == SortOrder.NAME_ASC },

        //        new() { Text = "Name (DESC)", Value = SortOrder.NAME_DESC.ToString(), Selected = model.SortOrder == SortOrder.NAME_DESC }
        //    };

        //    var result = new ClientClassesViewModel
        //    {
        //        Client = entity,

        //        Classes = classes,
        //    };

        //    return View(result);
        //}

        [HttpGet]
        public async Task<IActionResult> GetClassesByClientId(int id, string? keyword, SortOrder sortOrder, int pageIndex = 1, int pageSize = 5)
        {
            var entity = await _adminHttpClientService.GetClientByIdAsync(id);

            if (entity is null) return RedirectToAction("Forbidden", "Login", new { area = "" });

            var model = new ClassesClientPageViewModel
            {
                ClientId = entity.Id,

                Keyword = keyword,

                SortOrder = sortOrder,

                PageIndex = pageIndex,

                PageSize = pageSize
            };

            ViewBag.Keyword = keyword;

            ViewBag.SortOrder = SortOrderList(model);

            var classes = await _clientHttpService.GetClassesByClientIdAsync(model);

            var result = new ClientClassesViewModel
            {
                Client = entity,

                Classes = classes,
            };

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateClient(int id)
        {
            var entity = await _adminHttpClientService.GetClientByIdAsync(id);

            if (entity is null) return RedirectToAction("Forbidden", "Login", new { area = "" });

            return View(new UpdateClientViewModel
            {
                Id = entity.Id,

                UserName = entity.UserName,

                Firstname = entity.Firstname,

                Lastname = entity.Lastname,

                DateOfBirth = entity.DateOfBirth,

                DepartmentName = entity.DepartmentName,

                Address = entity.Address
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateClient(int id, UpdateClientViewModel model)
        {
            var result = await _adminHttpClientService.UpdateClientAsync(id, model);

            if (result is HttpResponseMessage)
            {
                await ModelStateHandler(result, "Cập nhật");

                return Json(new { isValid = false, html = await Helper.RenderRazorViewToString(this, "UpdateClient", model) });
            }

            var entity = await _adminHttpClientService.GetClientByIdAsync(id);

            return Json(new { isValid = true, html = await Helper.RenderRazorViewToString(this, "_ClientDetailPartialView", entity) });
        }

        [HttpGet]
        public IActionResult CreateClient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClient(CreateClientViewModel model)
        {
            var result = await _adminHttpClientService.CreateClientAsync(model);

            if (result is HttpResponseMessage)
            {
                await ModelStateHandler(result, "Tạo mới");

                return Json(new { isValid = false, html = await Helper.RenderRazorViewToString(this, "_CreateClientPartialView", model) });
            }

            var pageResponse = new ClientRolePageViewModel()
            {
                PageIndex = 1,

                PageSize = 5,

                IsDisabled = false
            };

            var data = await _adminHttpClientService.GetClientsByRoleNameAsync(pageResponse);

            return Json(new { isValid = true, html = await Helper.RenderRazorViewToString(this, "_IndexPartialView", data) });
        }

        /// <summary>
        /// Hàm xử lí lỗi validate từ server trả về
        /// </summary>
        /// <param name="obj">UserErrorViewModel</param>
        /// <param name="message"></param>
        /// <returns>Các lỗi validate ứng với từng field</returns>
        private async Task ModelStateHandler(object obj, string message)
        {
            var error = (HttpResponseMessage)obj;

            if (error.StatusCode != HttpStatusCode.OK)
            {
                UserErrorViewModel viewModel = new();

                var errorInfo = await HandleError<UserErrorViewModel>.HandleModelState(error, viewModel);

                int count = 0;

                foreach (PropertyInfo field in errorInfo.PropertyInfos)
                {
                    var value = field.GetValue(errorInfo.Value);

                    if (value is not null) ModelState.AddModelError(field.Name, value.ToString());

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
