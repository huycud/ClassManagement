using System.Net;
using System.Reflection;
using System.Security.Claims;
using ClassManagement.Mvc.Integrations.Users.Client;
using ClassManagement.Mvc.Integrations.Users.Teacher;
using ClassManagement.Mvc.Models.Clients;
using ClassManagement.Mvc.Models.Common.Errors;
using ClassManagement.Mvc.Models.User;
using ClassManagement.Mvc.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Common;

namespace ClassManagement.Mvc.Areas.Teacher.Controllers
{
    [Authorize(Roles = RoleConstants.TEACHER_NAME)]
    [Area(AreaConstants.TEACHER_AREA)]
    public class ProfileController(IClientHttpService clientHttpService, ITeacherHttpClientService teacherHttpClientService) : Controller
    {
        private readonly IClientHttpService _clientHttpService = clientHttpService;

        private readonly ITeacherHttpClientService _teacherHttpClientService = teacherHttpClientService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _ = Int32.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out int id);

            var entity = await _clientHttpService.GetByIdAsync(id);

            if (entity is null) return RedirectToAction("Index", "Login", new { area = "" });

            return View(entity);
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePassword(int id)
        {
            var entity = await _clientHttpService.GetByIdAsync(id);

            if (entity is null) return RedirectToAction("Index", "Login", new { area = "" });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(int id, UpdatePasswordViewModel model)
        {
            var result = await _clientHttpService.UpdatePasswordAsync(id, model);

            if (result.GetType() == typeof(HttpResponseMessage))
            {
                await ModelStateHandler(result, "Update password");

                return Json(new { isValid = false, html = await Helper.RenderRazorViewToString(this, "UpdatePassword", model) });
            }

            var entity = await _clientHttpService.GetByIdAsync(id);

            return Json(new { isValid = true, html = await Helper.RenderRazorViewToString(this, "_IndexPartialView", entity) });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var entity = await _clientHttpService.GetByIdAsync(id);

            if (entity is null) return RedirectToAction("Index", "Login", new { area = "" });

            return View(new UpdateClientViewModel()
            {
                Firstname = entity.Firstname,

                Lastname = entity.Lastname,

                Address = entity.Address,

                DateOfBirth = entity.DateOfBirth,

                DepartmentName = entity.DepartmentName
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdateClientViewModel model)
        {
            var result = await _clientHttpService.UpdateAsync(id, model);

            if (result.GetType() == typeof(HttpResponseMessage))
            {
                await ModelStateHandler(result, "Update");

                return Json(new { isValid = false, html = await Helper.RenderRazorViewToString(this, "Update", model) });
            }

            var user = await _clientHttpService.GetByIdAsync(id);

            return Json(new { isValid = true, html = await Helper.RenderRazorViewToString(this, "_IndexPartialView", user) });
        }

        [HttpGet]
        public IActionResult UpdateAvatar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAvatar(int id, UpdateImageViewModel model)
        {
            var result = await _teacherHttpClientService.UpdateAvatarAsync(id, model);

            if (result.GetType() == typeof(HttpResponseMessage))
            {
                await ModelStateHandler(result, "Update avatar");

                return Json(new { isValid = false, html = await Helper.RenderRazorViewToString(this, "UpdateAvatar", model) });
            }

            var user = await _clientHttpService.GetByIdAsync(id);

            return Json(new { isValid = true, html = await Helper.RenderRazorViewToString(this, "_IndexPartialView", user) });
        }

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
