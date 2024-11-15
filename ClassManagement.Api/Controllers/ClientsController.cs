using System.Net;
using ClassManagement.Api.DTO.Authentication;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Sender;
using ClassManagement.Api.DTO.Users;
using ClassManagement.Api.DTO.Users.Clients;
using ClassManagement.Api.Services.AppRole;
using ClassManagement.Api.Services.Email;
using ClassManagement.Api.Services.Users.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Common;

namespace ClassManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController(IClientService clientService, IRoleService roleService, IEmailService emailService,

                                    IConfiguration configuration, ILogger<ClientsController> logger) : ControllerBase
    {
        private readonly IClientService _clientService = clientService;

        private readonly IRoleService _roleService = roleService;

        private readonly IEmailService _emailService = emailService;

        private readonly IConfiguration _configuration = configuration;

        private readonly ILogger<ClientsController> _logger = logger;

        /// <summary>
        /// Get Client Info By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Policy = PolicyConstants.REQUIRELOWESTROLES)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInformation("Get client {@id}", id);

            if (id <= 0) return BadRequest();

            var result = await _clientService.GetByIdAsync(id);

            if (result is null) return NotFound();

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(result);
        }

        /// <summary>
        /// Get Clients By Role Name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = RoleConstants.ADMIN_NAME)]
        [HttpGet("get-clients-by-role")]
        public async Task<IActionResult> GetClientsByRoleNameAsync([FromQuery] ClientsRolePageRequest request)
        {
            _logger.LogInformation("Get clients by roleName with {@request}", request);

            var result = await _clientService.GetClientsByRoleNameAsync(request);

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(result);
        }

        /// <summary>
        /// Get Clients By Role Name
        /// </summary>
        /// <remarks>
        /// No Pagination
        /// </remarks>
        /// <param name="roleName"></param>
        /// <returns></returns>
        //[Authorize(Roles = RoleName.ADMIN)]
        //[HttpGet("get-by-role")]
        //public async Task<IActionResult> GetClientsAsync([FromQuery] string roleName)
        //{
        //    if (string.IsNullOrEmpty(roleName)) return BadRequest();

        //    var result = await _clientService.GetClientsByRoleNameAsync(roleName);

        //    return Ok(result);
        //}

        /// <summary>
        /// Get Clients By Class Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Policy = PolicyConstants.REQUIRELOWERROLES)]
        [HttpGet("get-clients-by-class")]
        public async Task<IActionResult> GetClientsByClassIdAsync([FromQuery] ClientsClassPageRequest request)
        {
            _logger.LogInformation("Get clients by classId with {@request}", request);

            if (string.IsNullOrEmpty(request.ClassId)) return BadRequest();

            var result = await _clientService.GetClientsByClassIdAsync(request);

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(result);
        }

        /// <summary>
        /// Create A New Client
        /// </summary>
        /// <remarks>
        /// Using a new CreateClientRequest model
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = RoleConstants.ADMIN_NAME)]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateClientRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Create client with {@request}", request);

            var clientId = await _clientService.CreateClientAsync(request, cancellationToken);

            if (clientId <= 0) return BadRequest();

            var entity = await _clientService.GetByIdAsync(clientId);

            _logger.LogInformation(string.Format("POST RESULT = {0}", HttpStatusCode.OK));

            return Created(new Uri($"{_configuration["BaseAddress"]}/api/clients/{clientId}"), entity);
        }

        /// <summary>
        /// Update The Client's Profile
        /// </summary>
        /// <remarks>
        /// Using a UpdateClientRequest new model
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Policy = PolicyConstants.REQUIRELOWESTROLES)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] UpdateClientRequest request)
        {
            _logger.LogInformation("Update client {@id} with {@request}", id, request);

            if (id <= 0) return BadRequest();

            var result = await _clientService.UpdateClientAsync(id, request);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("PUT RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }

        /// <summary>
        /// Update The Password For The Client Account
        /// </summary>
        /// <remarks>
        /// Using a new UpdatePasswordRequest model
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Policy = PolicyConstants.REQUIRELOWESTROLES)]
        [HttpPut("update-password/{id}")]
        public async Task<IActionResult> UpdatePasswordAsync(int id, [FromBody] UpdatePasswordRequest request)
        {
            _logger.LogInformation("Update password for client {@id} with {@request}", id, request);

            if (id <= 0) return BadRequest();

            var result = await _clientService.UpdatePasswordAsync(id, request);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("PUT RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Forgot password for email @{request}", request);

            var result = await _clientService.ForgotPasswordAsync(request, cancellationToken);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("POST RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
        {
            _logger.LogInformation("Reset password with {@request}", request);

            var result = await _clientService.ResetPasswordAsync(request);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("POST RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }

        /// <summary>
        /// Update The Client's Avatar
        /// </summary>
        /// <remarks>
        /// Using a new UpdateImageRequest model
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Policy = PolicyConstants.REQUIRELOWESTROLES)]
        [HttpPut("update-avatar/{id}")]
        public async Task<IActionResult> UpdateImageAsync(int id, [FromForm] UpdateImageRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Update avatar for client {@id} with {@request}", id, request);

            if (id <= 0) return BadRequest();

            var result = await _clientService.UpdateImageAsync(id, request, cancellationToken);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("PUT RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }

        #region Test Api
        //[Authorize(Roles = RoleConstants.ADMIN_NAME)]
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        //{
        //    _logger.LogDebug("This is a api test.....");

        //    _logger.LogInformation("Delete client {@id}", id);

        //    if (string.IsNullOrEmpty(id.ToString())) return BadRequest();

        //    var result = await _clientService.DeleteAsync(id, cancellationToken);

        //    if (!result) return BadRequest();

        //    _logger.LogInformation(string.Format("POST RESULT = {0}", HttpStatusCode.OK));

        //    return NoContent();
        //}
        #endregion

        /// <summary>
        /// Get Roles
        /// </summary>
        /// <remarks>
        /// Using user id
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RoleConstants.ADMIN_NAME)]
        [HttpGet("get-roles/{id}")]
        public async Task<IActionResult> GetRolesByUserIdAsync(int id)
        {
            _logger.LogInformation("Get roles by user {@id}", id);

            if (id <= 0) return BadRequest();

            var result = await _roleService.GetRolesByUserIdAsync(id);

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(result);
        }

        /// <summary>
        /// Confirm Email After User Registers 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] ConfirmEmailRequest request)
        {
            _logger.LogInformation("Confirm email with {@request}", request);

            var result = await _emailService.ConfirmEmailAsync(request);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }
    }
}
