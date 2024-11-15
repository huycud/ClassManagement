using System.Net;
using ClassManagement.Api.DTO.AppRole;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.Services.AppRole;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Common;

namespace ClassManagement.Api.Controllers
{
    [Authorize(Roles = RoleConstants.ADMIN_NAME)]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRoleService roleService, IConfiguration configuration, ILogger<RolesController> logger) : ControllerBase
    {
        private readonly IRoleService _roleService = roleService;

        private readonly IConfiguration _configuration = configuration;

        private readonly ILogger<RolesController> _logger = logger;

        /// <summary>
        /// Get Roles
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] CommonPageRequest request)
        {
            _logger.LogInformation("Get roles with {@request}", request);

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(await _roleService.GetAsync(request));
        }

        /// <summary>
        /// Get Role By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInformation("Get role {@id}", id);

            if (id <= 0) return BadRequest();

            var role = await _roleService.GetByIdAsync(id);

            if (role is null) return NotFound();

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(role);
        }

        /// <summary>
        /// Create A New Role
        /// </summary>
        /// <remarks>
        /// Using a new CreateRoleRequest model
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRoleRequest request)
        {
            _logger.LogInformation("Create role with {@request}", request);

            var roleId = await _roleService.CreateAsync(request);

            if (roleId <= 0) return BadRequest();

            _logger.LogInformation(string.Format("POST RESULT = {0}", HttpStatusCode.OK));

            return Created(new Uri($"{_configuration["BaseAddress"]}/api/roles/{roleId}"), await _roleService.GetByIdAsync(roleId));
        }

        /// <summary>
        /// Update Role
        /// </summary>
        /// <remarks>
        /// Using a new UpdateRoleRequest model
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateRoleRequest request)
        {
            _logger.LogInformation("Update role {@id} with {@request}", id, request);

            if (id <= 0) return BadRequest();

            if (!await _roleService.UpdateAsync(id, request)) return BadRequest();

            _logger.LogInformation(string.Format("PUT RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }

        /// <summary>
        /// Delete Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation("Delete role {@id}", id);

            if (id <= 0) return BadRequest();

            if (!await _roleService.DeleteAsync(id)) return BadRequest();

            _logger.LogInformation(string.Format("DELETE RESULT = {0}", HttpStatusCode.OK));

            return NoContent();
        }
    }
}
