using System.Net;
using System.Reflection;
using System.Security.Claims;
using ClassManagement.Mvc.Integrations.Users.Manager;
using ClassManagement.Mvc.Models.Common.Errors;
using ClassManagement.Mvc.Models.Manager;
using ClassManagement.Mvc.Models.User;
using ClassManagement.Mvc.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Common;

namespace ClassManagement.Mvc.Areas.Admin.Controllers
{
    [Area(AreaConstants.ADMIN_AREA)]
    [Authorize(Roles = RoleConstants.ADMIN_NAME)]
    public class ProfileController(IAdminHttpClientService adminHttpClientService) : Controller
    {
        private readonly IAdminHttpClientService _adminHttpClientService = adminHttpClientService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _ = Int32.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int id);

            var entity = await _adminHttpClientService.GetByIdAsync(id);

            if (entity is null) return RedirectToAction("Index", "Login", new { area = "" });

            return View(entity);
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePassword(int id)
        {
            var entity = await _adminHttpClientService.GetByIdAsync(id);

            if (entity is null) return RedirectToAction("Index", "Login", new { area = "" });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(int id, UpdatePasswordViewModel model)
        {
            var result = await _adminHttpClientService.UpdatePasswordAsync(id, model);

            if (result is HttpResponseMessage)
            {
                await ModelStateHandler(result, "Update password");

                return Json(new { isValid = false, html = await Helper.RenderRazorViewToString(this, "UpdatePassword", model) });
            }

            var entity = await _adminHttpClientService.GetByIdAsync(id);

            return Json(new { isValid = true, html = await Helper.RenderRazorViewToString(this, "_IndexPartialView", entity) });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var entity = await _adminHttpClientService.GetByIdAsync(id);

            if (entity is null) return RedirectToAction("Index", "Login", new { area = "" });

            return View(new UpdateAdminViewModel()
            {
                Email = entity.Email,

                UserName = entity.UserName,

                Fullname = entity.Fullname
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdateAdminViewModel model)
        {
            var result = await _adminHttpClientService.UpdateAsync(id, model);

            if (result is HttpResponseMessage)
            {
                await ModelStateHandler(result, "Update");

                return Json(new { isValid = false, html = await Helper.RenderRazorViewToString(this, "Update", model) });
            }

            var user = await _adminHttpClientService.GetByIdAsync(id);

            return Json(new { isValid = true, html = await Helper.RenderRazorViewToString(this, "_IndexPartialView", user) });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAvatar(int id)
        {
            var entity = await _adminHttpClientService.GetByIdAsync(id);

            if (entity is null) return RedirectToAction("Index", "Login", new { area = "" });

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAvatar(int id, UpdateImageViewModel model)
        {
            var result = await _adminHttpClientService.UpdateAvatarAsync(id, model);

            if (result is HttpResponseMessage)
            {
                await ModelStateHandler(result, "Update avatar");

                return Json(new { isValid = false, html = await Helper.RenderRazorViewToString(this, "UpdateAvatar", model) });
            }

            var user = await _adminHttpClientService.GetByIdAsync(id);

            return Json(new { isValid = true, html = await Helper.RenderRazorViewToString(this, "_IndexPartialView", user) });
        }
        /// <summary>
        /// Hàm xử lí lỗi validate gửi từ server trả về
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

                if (count == errorInfo.PropertyInfos.Length) ModelState.AddModelError("", $"{message} fail. Please check information again");
            }
        }
    }
}
