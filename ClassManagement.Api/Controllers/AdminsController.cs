using System.Net;
using ClassManagement.Api.DTO.Authentication;
using ClassManagement.Api.DTO.Common;
using ClassManagement.Api.DTO.Users;
using ClassManagement.Api.DTO.Users.Manager;
using ClassManagement.Api.Services.Users.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Common;

namespace ClassManagement.Api.Controllers
{
    [Authorize(Roles = RoleConstants.ADMIN_NAME)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController(IAdminService adminService, IConfiguration configuration, ILogger<AdminsController> logger) : ControllerBase
    {
        private readonly IAdminService _adminService = adminService;

        private readonly IConfiguration _configuration = configuration;

        private readonly ILogger<AdminsController> _logger = logger;

        /// <summary>
        /// Get Admin Info By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInformation("Get admin {@id}", id);

            if (id <= 0) return BadRequest();

            var result = await _adminService.GetByIdAsync(id);

            if (result is null) return NotFound();

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(result);
        }

        #region Api Test
        /// <summary>
        /// Create A New Admin - Api Test
        /// </summary>
        /// <remarks>
        /// Using a new CreateAdminRequest model
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAdminAsync([FromBody] CreateAdminRequest request)
        {
            _logger.LogDebug("This is a api test.....");

            _logger.LogInformation("Create admin account with {@request}", request);

            var userId = await _adminService.CreateAsync(request);

            if (userId < 0) return BadRequest();

            _logger.LogInformation(string.Format("POST RESULT = {0}", HttpStatusCode.OK));

            return Created(new Uri($"{_configuration["BaseAddress"]}/api/admins/{userId}"), await _adminService.GetByIdAsync(userId));
        }
        #endregion

        /// <summary>
        /// Update The Admin's Profile
        /// </summary>
        /// <remarks>
        /// Using a new UpdateAdminRequest model
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateAdminRequest request)
        {
            _logger.LogInformation("Update admin {@id} with {@request}", id, request);

            if (id <= 0) return BadRequest();

            var result = await _adminService.UpdateAsync(id, request);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("PUT RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }

        /// <summary>
        /// Update The Password For The Admin Account
        /// </summary>
        /// <remarks>
        /// Using a new UpdatePasswordRequest model
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update-password/{id}")]
        public async Task<IActionResult> UpdatePasswordAsync(int id, [FromBody] UpdatePasswordRequest request)
        {
            _logger.LogInformation("Update password for admin {@id} with {@request}", id, request);

            if (id <= 0) return BadRequest();

            var result = await _adminService.UpdatePasswordAsync(id, request);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("PUT RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }

        /// <summary>
        /// Update The Admin's Avatar
        /// </summary>
        /// <remarks>
        /// Using a new UpdateImageRequest model
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update-avatar/{id}")]
        public async Task<IActionResult> UpdateImageAsync(int id, [FromForm] UpdateImageRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Update avatar for admin {@id} with {@request}", id, request);

            if (id <= 0) return BadRequest();

            var result = await _adminService.UpdateImageAsync(id, request, cancellationToken);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("PUT RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }

        /// <summary>
        /// Get A Token To Reset Password When Forgot Password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Forgot password for admin with {@request}", request);

            var result = await _adminService.ForgotPasswordAsync(request, cancellationToken);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("POST RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <remarks>
        /// Using A ResetPasswordRequest model
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
        {
            _logger.LogInformation("Reset password for admin with {@request}", request);

            var result = await _adminService.ResetPasswordAsync(request);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("POST RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }

        #region Api test
        //[HttpPost("{id}/add-role")]
        //public async Task<IActionResult> AddRoleAsync(int id, [FromBody] RoleRequest request)
        //{
        //    if (string.IsNullOrEmpty(id.ToString())) return BadRequest();

        //    var result = await _adminService.AddRoleAsync(id, request);

        //    if (!result) return BadRequest();

        //    return Ok();
        //}

        //[HttpPost("{id}/remove-role")]
        //public async Task<IActionResult> RemoveRoleAsync(int id, [FromBody] RoleRequest request)
        //{
        //    if (string.IsNullOrEmpty(id.ToString())) return BadRequest();

        //    var result = await _adminService.RemoveRoleAsync(id, request);

        //    if (!result) return BadRequest();

        //    return Ok();
        //}

        #endregion

        /// <summary>
        /// Disable The Client's Account
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("disable-account/{id}")]
        public async Task<IActionResult> DisableAsync(int id, [FromBody] DisableAccountRequest request)
        {
            _logger.LogDebug("This is a api test.....");

            _logger.LogInformation("Disable account {@id} with {@request}", id, request);

            if (id <= 0) return BadRequest();

            var result = await _adminService.DisableAccountAsync(id, request);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("PUT RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }
    }
}
