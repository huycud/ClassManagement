using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using ClassManagement.Mvc.Integrations.Authenticate;
using ClassManagement.Mvc.Models.Authentication;
using ClassManagement.Mvc.Models.Common;
using ClassManagement.Mvc.Models.Common.Errors;
using ClassManagement.Mvc.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Utilities.Common;
using Utilities.Messages;

namespace ClassManagement.Mvc.Controllers
{
    public class AccountController(IAuthHttpClientService authService, IConfiguration configuration) : Controller
    {
        private readonly IAuthHttpClientService _authService = authService;

        private readonly IConfiguration _configuration = configuration;

        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            var session = HttpContext.Session.GetString(SystemConstants.ACCESSTOKEN_NAME);

            if (!string.IsNullOrEmpty(session))
            {
                return RedirectToAreaWithClaimsPrincipal(HttpContext.User, returnUrl);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                return View();
            }

            if (!ModelState.IsValid) return View(ModelState);

            var result = await _authService.LoginAsync(model);

            if (result.GetType() == typeof(HttpResponseMessage))
            {
                await ModelStateHandler(result, string.Format(ErrorMessages.LOCKED, "Account"));

                return View();
            }

            var data = (Response)result;

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),

                IsPersistent = true
            };

            var userPrincipal = ValidateToken.TokenValidation(data.AccessToken, _configuration);

            HttpContext.Session.SetString(SystemConstants.ACCESSTOKEN_NAME, data.AccessToken);

            HttpContext.Session.SetString(SystemConstants.REFRESHTOKEN_NAME, data.RefreshToken);

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),

                Path = "/",

                SameSite = SameSiteMode.None,

                Secure = true
            };

            HttpContext.Response.Cookies.Append(SystemConstants.COOKIE_NAME, data.UserId.ToString(), cookieOptions);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, authProperties);

            return RedirectToAreaWithClaimsPrincipal(userPrincipal, returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove(SystemConstants.ACCESSTOKEN_NAME);

            _ = Int32.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int id);

            var revokeResult = await _authService.LogoutAsync(id);

            if (revokeResult is HttpResponseMessage)
            {
                await ModelStateHandler(revokeResult, ErrorMessages.LOGOUT_PARTIAL_SUCCESS_MESSAGE);
            }

            return RedirectToAction("Login", "Account", new { area = "" });
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        private IActionResult RedirectToAreaWithClaimsPrincipal(ClaimsPrincipal claimsPrincipal, string? returnUrl)
        {
            if (claimsPrincipal.IsInRole(RoleConstants.ADMIN_NAME))
            {
                if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);

                return RedirectToAction("Index", "Home", new { area = AreaConstants.ADMIN_AREA });
            }

            else if (claimsPrincipal.IsInRole(RoleConstants.TEACHER_NAME))
            {
                if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);

                return RedirectToAction("Index", "Home", new { area = AreaConstants.TEACHER_AREA });
            }

            else if (claimsPrincipal.IsInRole(RoleConstants.STUDENT_NAME))
            {
                if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Forbidden", "Home");
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

                    if (value is not null)
                    {
                        var isConfirmed = value.ToString().ToLower().Contains("confirmed") || value.ToString().ToLower().Contains("RefreshToken");

                        if (isConfirmed) ModelState.AddModelError("", value.ToString());

                        else ModelState.AddModelError(field.Name, value.ToString());
                    }

                    else count++;
                }

                if (count == errorInfo.PropertyInfos.Length) ModelState.AddModelError("", message);
            }
        }
    }
}
