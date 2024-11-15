using System.Net;
using System.Reflection;
using System.Security.Claims;
using ClassManagement.Mvc.Integrations.Notify;
using ClassManagement.Mvc.Models.Common;
using ClassManagement.Mvc.Models.Common.Errors;
using ClassManagement.Mvc.Models.Notify;
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
    public class NotifyController(INotifyHttpClientService notifyHttpClientService) : Controller
    {
        private readonly INotifyHttpClientService _notifyHttpClientService = notifyHttpClientService;

        [HttpGet]
        public async Task<IActionResult> Index(SortOrder sortOrder, int pageIndex = 1, int pageSize = 5)
        {
            var resultSystemType = await _notifyHttpClientService.GetNotifiesAsync(new NotifyPageViewModel
            {
                SortOrder = sortOrder,

                PageIndex = pageIndex,

                PageSize = pageSize,

                Type = NotifyType.System
            });

            var resultClassroomType = await _notifyHttpClientService.GetNotifiesAsync(new NotifyPageViewModel
            {
                SortOrder = sortOrder,

                PageIndex = pageIndex,

                PageSize = pageSize,

                Type = NotifyType.ClassRoom
            });

            var resultClassType = await _notifyHttpClientService.GetNotifiesAsync(new NotifyPageViewModel
            {
                SortOrder = sortOrder,

                PageIndex = pageIndex,

                PageSize = pageSize,

                Type = NotifyType.Class
            });

            return View(new Tuple<PageResultViewModel<NotifyViewModel>, PageResultViewModel<NotifyViewModel>, PageResultViewModel<NotifyViewModel>>(resultSystemType, resultClassroomType, resultClassType));
        }

        [HttpGet]
        public async Task<IActionResult> GetListByType(NotifyType type, SortOrder sortOrder, int pageIndex = 1, int pageSize = 10)
        {
            var model = new NotifyPageViewModel
            {
                SortOrder = sortOrder,

                PageIndex = pageIndex,

                PageSize = pageSize,

                Type = type
            };

            var result = await _notifyHttpClientService.GetNotifiesAsync(model);

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifiesByUserId(NotifyType? type, string? keyword, SortOrder sortOrder, int pageIndex = 1, int pageSize = 10)
        {
            _ = Int32.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);

            var model = new NotifyPageViewModel
            {
                UserId = userId,

                Keyword = keyword,

                SortOrder = sortOrder,

                PageIndex = pageIndex,

                PageSize = pageSize,

                Type = type
            };

            ViewBag.Keyword = keyword;

            ViewBag.Types = SortTypeList(model);

            ViewBag.SortOrder = SortOrderList(model);

            var result = await _notifyHttpClientService.GetNotifiesAsync(model);

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifiesDeleted(NotifyType? type, string? keyword, SortOrder sortOrder, int pageIndex = 1, int pageSize = 10)
        {
            _ = Int32.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);

            var model = new NotifyPageViewModel
            {
                UserId = userId,

                Keyword = keyword,

                SortOrder = sortOrder,

                PageIndex = pageIndex,

                PageSize = pageSize,

                Type = type,

                IsDeleted = true
            };

            ViewBag.Keyword = keyword;

            ViewBag.Types = SortTypeList(model);

            ViewBag.SortOrder = SortOrderList(model);

            var result = await _notifyHttpClientService.GetNotifiesAsync(model);

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifyById(string id)
        {
            var entity = await _notifyHttpClientService.GetByIdAsync(id);

            if (entity is null) return RedirectToAction("Index", "Login", new { area = "" });

            return View(entity);
        }

        [HttpGet]
        public IActionResult CreateNotify()
        {
            ViewBag.NotifyTypes = new SelectList(RenameNotifyType(), "Value", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNotify(CreateNotifyViewModel model)
        {
            model.UserId = Int32.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId) ? userId : 0;

            var result = await _notifyHttpClientService.CreateNotifyAsync(model);

            if (result is HttpResponseMessage)
            {
                await ModelStateHandler(result, "Tạo tin");

                ViewBag.NotifyTypes = new SelectList(RenameNotifyType(), "Value", "Text");

                return Json(new { isValid = false, html = await Helper.RenderRazorViewToString(this, "_CreateNotifyPartialView", model) });
            }

            return Json(new { isValid = true, isReload = true });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateNotify(string id)
        {
            var entity = await _notifyHttpClientService.GetByIdAsync(id);

            if (entity is null) return RedirectToAction("Forbidden", "Login");

            return View(new UpdateNotifyViewModel
            {
                Id = entity.Id,

                Title = entity.Title,

                Content = entity.Content,

                Type = Enum.TryParse(entity.Type, out NotifyType type) ? type : default
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateNotify(string id, UpdateNotifyViewModel model)
        {
            var result = await _notifyHttpClientService.UpdateNotifyAsync(id, model);

            if (result is HttpResponseMessage)
            {
                await ModelStateHandler(result, "Update notify");

                return Json(new { isValid = false, html = await Helper.RenderRazorViewToString(this, "UpdateNotify", model) });
            }

            var entity = await _notifyHttpClientService.GetByIdAsync(id);

            return Json(new { isValid = true, html = await Helper.RenderRazorViewToString(this, "_NotifyDetailPartialView", entity) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatusNotify(string id, ChangeNotifyStatusViewModel model)
        {
            model.UserId = Int32.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId) ? userId : 0;

            var result = await _notifyHttpClientService.ChangeStatusNotifyAsync(id, model);

            if (!result) return Json(new { isValid = false });

            var pageModel = new NotifyPageViewModel
            {
                UserId = userId,

                PageIndex = 1,

                PageSize = 10,

                IsDeleted = !model.IsDeleted
            };

            var resultPage = await _notifyHttpClientService.GetNotifiesAsync(pageModel);

            return Json(new
            {
                isValid = true,

                msg = model.IsDeleted ? "Xóa" : "Khôi phục",

                html = await Helper.RenderRazorViewToString(this, model.IsDeleted ? "_GetNotifiesByUserIdPartialView" : "_GetNotifiesDeletedPartialView", resultPage)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteNotify(string id, SortOrder sortOrder, int pageIndex = 1, int pageSize = 5)
        {
            var result = await _notifyHttpClientService.DeleteNotifyAsync(id);

            if (!result) return Json(new { isValid = false });

            _ = Int32.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);

            var model = new NotifyPageViewModel
            {
                UserId = userId,

                PageIndex = pageIndex,

                PageSize = pageSize,

                IsDeleted = true,

                SortOrder = sortOrder
            };

            var resultPage = await _notifyHttpClientService.GetNotifiesAsync(model);

            return Json(new { isValid = true, html = await Helper.RenderRazorViewToString(this, "_GetNotifiesDeletedPartialView", resultPage) });
        }

        /// <summary>
        /// Error Handling Function Returned From The Server
        /// </summary>
        /// <param name="obj">NotifyErrorViewModel</param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task ModelStateHandler(object obj, string message)
        {
            var error = (HttpResponseMessage)obj;

            if (error.StatusCode != HttpStatusCode.OK)
            {
                NotifyErrorViewModel viewModel = new();

                var errorInfo = await HandleError<NotifyErrorViewModel>.HandleModelState(error, viewModel);

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

        private List<SelectedItems> RenameNotifyType()
        {
            return new List<SelectedItems>
            {
                new() { Value = NotifyType.System.ToString(), Text = "Hệ thống" },

                new() { Value = NotifyType.Class.ToString(), Text = "Lớp học" },

                new() { Value = NotifyType.ClassRoom.ToString(), Text = "Phòng học" },
            };
        }

        private List<SelectListItem> SortOrderList(CommonPageViewModel model)
        {
            return new List<SelectListItem>
            {
                new() { Text = "TITLE (ASC)", Value = SortOrder.AscendingName.ToString(), Selected = model.SortOrder == SortOrder.AscendingName },

                new() { Text = "TITLE (DESC)", Value = SortOrder.DescendingName.ToString(), Selected = model.SortOrder == SortOrder.DescendingName }
            };
        }

        private IEnumerable<SelectListItem> SortTypeList(NotifyPageViewModel model)
        {
            var typeList = new Dictionary<string, NotifyType> {

                { "Hệ thống", NotifyType.System },

                { "Lớp học" , NotifyType.Class},

                { "Phòng học",NotifyType.ClassRoom} };

            return typeList.Select(x => new SelectListItem
            {
                Text = x.Key,

                Value = x.Value.ToString(),

                Selected = !string.IsNullOrEmpty(model.Type.ToString()) && model.Type == x.Value
            });
        }
    }
}
