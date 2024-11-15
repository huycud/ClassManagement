using System.Net;
using ClassManagement.Api.DTO.Class;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.Services.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Common;

namespace ClassManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController(IClassService classService, IConfiguration configuration, ILogger<ClassesController> logger) : ControllerBase
    {
        private readonly IClassService _classService = classService;

        private readonly IConfiguration _configuration = configuration;

        private readonly ILogger<ClassesController> _logger = logger;

        /// <summary>
        /// Get Class List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = RoleConstants.ADMIN_NAME)]
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] ClassPageRequest request)
        {
            _logger.LogInformation("Get classes with {@request}", request);

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(await _classService.GetAsync(request));
        }

        /// <summary>
        /// Get Class List By Client Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Policy = PolicyConstants.REQUIREALLROLES)]
        [HttpGet("get-by-client")]
        public async Task<IActionResult> GetByClientIdAsync([FromQuery] ClassesClientPageRequest request)
        {
            _logger.LogInformation("Get classes by clientId with {@request}", request);

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(await _classService.GetByClientIdAsync(request));
        }
        /// <summary>
        /// Get Class By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Policy = PolicyConstants.REQUIRELOWERROLES)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            _logger.LogInformation("Get class {@id}", id);

            if (string.IsNullOrEmpty(id.ToString())) return BadRequest();

            var result = await _classService.GetByIdAsync(id);

            if (result is null) return NotFound();

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(result);
        }

        /// <summary>
        /// Create A New Class
        /// </summary>
        /// <remarks>
        /// Using a new CreateClassRequest model
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = RoleConstants.ADMIN_NAME)]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateClassRequest request)
        {
            _logger.LogInformation("Create class with {@request}", request);

            var classId = await _classService.CreateAsync(request);

            if (string.IsNullOrEmpty(classId)) return BadRequest();

            _logger.LogInformation(string.Format("POST RESULT = {0}", HttpStatusCode.OK));

            return Created(new Uri($"{_configuration["BaseAddress"]}/api/classes/{classId}"), await _classService.GetByIdAsync(classId));
        }

        /// <summary>
        /// Update The Class's Info
        /// </summary>
        /// <remarks>
        /// Using a new UpdateClassRequest model
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = RoleConstants.ADMIN_NAME)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromForm] UpdateClassRequest request)
        {
            _logger.LogInformation("Update class {@id} with {@request}", id, request);

            if (string.IsNullOrEmpty(id)) return BadRequest();

            var result = await _classService.UpdateAsync(id, request);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("PUT RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }

        /// <summary>
        /// Delete A Class - Test Api
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RoleConstants.ADMIN_NAME)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            _logger.LogDebug("This is a api test....");

            _logger.LogInformation("Delete class {@id}", id);

            if (string.IsNullOrEmpty(id)) return BadRequest();

            var result = await _classService.DeleteAsync(id);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("DELETE RESULT = {0}", HttpStatusCode.OK));

            return NoContent();
        }

        /// <summary>
        /// Get Students Who Are Not Exists In Class
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Policy = PolicyConstants.REQUIRELOWERROLES)]
        [HttpGet("get-students-not-exist-in-class/{id}")]
        public async Task<IActionResult> GetStudentsNotExistInClassAsync(string id, [FromQuery] UserPageRequest request)
        {
            _logger.LogInformation("Get students not exist in class {@id} with {@request}", id, request);

            if (string.IsNullOrEmpty(id)) return BadRequest();

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(await _classService.GetStudentsNotExistInClassAsync(id, request));
        }

        /// <summary>
        /// Add Students In Class
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Policy = PolicyConstants.REQUIRELOWERROLES)]
        [HttpPut("add-student-to-class/{id}")]
        public async Task<IActionResult> AddStudentToClassAsync(string id, [FromBody] List<int> request)
        {
            _logger.LogInformation("Add students {@request} to class {@id}", request, id);

            if (string.IsNullOrEmpty(id)) return BadRequest();

            var result = await _classService.AddStudentToClassAsync(id, request);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("PUT RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }
    }
}
